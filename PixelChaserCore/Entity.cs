using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace PixelChaser
{
    public abstract class Entity:PWObject,IDisposable,ICollision
    {

        public int HP { get; set; } = 1;
        public abstract string TextureID { get;}

        public PointF Position
        {
            get
            {
                return new PointF(X, Y);
            }
            set
            {
                if(value.X != X || value.Y != Y)
                {
                    X = value.X;
                    Y = value.Y;

                    OnPositionChanged();
                }
            }
        }
        public override double Angle
        {
            get
            {
                double alt = (AimY - Y);
                double wid = (AimX - X);
                double tFactor = alt / wid;
                return Math.Atan2(wid, alt);

            }
        }
        public float AimX
        {
            get { return _aimX; }
            set
            {
                if (value != _aimX)
                {
                    _aimX = value;
                    OnAngleChanged();
                }
            }
        }
        private float _aimX = 0;
        public float AimY
        {
            get { return _aimY; }
            set
            {
                if (value != _aimY)
                {
                    _aimY = value;
                    OnAngleChanged();
                }
            }
        }
        private float _aimY = 0;
        public int MoveInterval
        {
            get { return _moveTimer.Interval; }
            set
            {
                if (value > 1 && value < 5000)
                    _moveTimer.Interval = value;
            }
        }
        private Timer _moveTimer = new Timer();
        public Gun Gun
        {
            get
            {
                if (Arsenal == null)
                    return new NoGun(this);
                else if (Arsenal.Count == 0 || SelectedGun < 0)
                    return new NoGun(this);
                else if (SelectedGun >= Arsenal.Count)
                    _selectedGunIndex = Arsenal.Count - 1;
                return Arsenal[SelectedGun];
            }
        }
        public List<Gun> Arsenal { get; private set; } = new List<Gun>();
        public int SelectedGun
        {
            get { return _selectedGunIndex; }
            set
            {
                if (value < 0)
                    _selectedGunIndex = 0;
                else if (value >= Arsenal.Count)
                    _selectedGunIndex = Arsenal.Count - 1;
                else
                    _selectedGunIndex = value;
            }
        }
        private int _selectedGunIndex = 0;
        public float AirFactor { get; set; } = 0;
        public int LifeTime { get; private set; } = 0;
        public abstract float InitialX { get; }
        public abstract float InitialY { get; }
        public bool EnableProjectileCollisions { get; set; } = true;
        public int Hits { get { return Gun.Hits; } }
        public float Speed { get; set; } = 1;
        public bool VulnerableMyBullets { get; set; } = false;
        public virtual bool IsDead
        {
            get
            {
                return (HP <= 0);
            }
        }

        public virtual void Move_Tick(object sender, EventArgs e)
        {
           // CheckProjectileCollisions();
            CheckHits();
            UpdatePosition();
            UpdateVelocity();
        }


        public virtual void WorldTick(object sender, EventArgs e)
        {

            if (IsDead)
            {
                Dispose();
            }
            else
            {
                LifeTime++;
               // CheckProjectileCollisions();

            }
        }


        public virtual void EnterWorld(PixelWorld world)
        {
            //World = world;
            //World.MovedDown += WorldTick;

            LoadWorld(world);

            _moveTimer.Tick += Move_Tick;
            _moveTimer.Interval = 1;
            _moveTimer.Start();

            if (!world.Entities.Contains(this))
                world.Entities.Add(this);

            X = InitialX;
            Y = InitialY;

            CollisionData = new CollisionData(this);
            CollisionData.NewProjectileCollision += ProjectileCollision;

            OnWorldEntered();
        }

        private void Entity_WorldEntered(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ProjectileCollision(object sender, EventArgs e)
        {
            OnProjectileCollision( (Projectile)sender);
        }

        public virtual void OnProjectileCollision(Projectile proj)
        {            
            proj.EndTargets.Add(ID);

            if(proj.SourceID != ID || VulnerableMyBullets)
                HP = HP - proj.Damage;

            World.Projectiles.Remove(proj);
        }

        public virtual void UpdateVelocity()
        {
            Velocity.AreaDendistyFactor = World.AreaDensityFactor;
        }

        public void MoveUp(float power = 1)
        {
            power = power * Speed;
            Velocity.Y = Velocity.Y - power;
        }
        public void MoveDown(float power = 1)
        {
            power = power * Speed;
            Velocity.Y = Velocity.Y + power;
        }
        public void MoveLeft(float power = 1)
        {
            power = power * Speed;
            Velocity.X = Velocity.X - power;
        }
        public void MoveRight(float power = 1)
        {
            power = power * Speed;
            Velocity.X = Velocity.X + power;
        }
        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }


        public void CheckProjectileCollisions()
        {
            if (EnableProjectileCollisions)
                for (int i = 0; i < World.Projectiles.Count; i++)
                {
                        try
                        {
                            Projectile prj = World.Projectiles[i];
                            CollisionData.CheckProjectileCollision(prj);
                        }
                        catch { }
                    
                }
        }

        private void CheckHits()
        {
            //for (int pr = 0; pr < World.Projectiles.Count; pr++)
            //{
            //    Projectile p = World.Projectiles[pr];
            //    PointF pt1 = new PointF(p.X, p.Y);
            //    PointF pt2 = new PointF((p.X + p.Velocity.X), (p.Y + p.Velocity.Y));

            //    int hits = World.PixelUnits.RemoveAll(pu => xMath.LineIntersectsRect(pt1, pt2, pu.GetRectangle()));

            //    if (hits > 0)
            //    {
            //        p.AddHit(hits);
            //    }
            //}
        }
        private bool _disposed = false;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                if(World.Entities.Contains(this))
                 World.Entities.Remove(this);
                this._moveTimer.Dispose();
                foreach (Gun gun in Arsenal)
                    gun.Dispose();
                
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }
    }
}


