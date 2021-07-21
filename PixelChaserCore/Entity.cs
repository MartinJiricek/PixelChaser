using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PixelChaser
{
    public abstract class Entity
    {

        public int HP { get; set; } = 1;
        public PixelWorld CurrentWorld { get; private set; }
        public abstract string Name { get; }
        public abstract string TypeID { get; }
        public string TextureID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public double AimAngle
        {
            get
            {
                double alt = (AimY - Y);
                double wid = (AimX - X);
                double tFactor = alt / wid;
                return Math.Atan2(wid, alt);

            }
        }
        public float AimX { get; set; }
        public float AimY { get; set; }
        public float Width { get; set; } = 32;
        public float Height { get; set; } = 32;
        public Point RangeEndpoint
        {
            get
            {
                Point pt = new Point();

                double x = X + (Math.Cos(AimAngle) * Gun.Range);
                double y = Y + (Math.Sin(AimAngle) * Gun.Range);

                pt.X = (int)x;
                pt.Y = (int)y;

                return pt;
            }
        }
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
        public Velocity Velocity { get; set; } = new Velocity();
        public float AirFactor { get; set; } = 0;
        public int LifeTime { get; private set; } = 0;
        public abstract float InitialX { get; }
        public abstract float InitialY { get; }
        public bool EnableProjectileCollisions { get; set; } = true;
        public int Hits { get { return Gun.Hits; } }

        public float Speed { get; set; } = 1;

        public PointF TL
        {
            get
            {

                float x = (X - (Width / 2));
                float y = (Y - (Height / 2));
                float rotatedX = (float)(x * Math.Cos(AimAngle) - y * Math.Sin(AimAngle));
                float rotatedY = (float)(x * Math.Sin(AimAngle) + y * Math.Cos(AimAngle));

                return new PointF(rotatedX + X, rotatedY + y);
            }
        }
        public PointF TR
        {
            get
            {

                float x = (X + (Width / 2));
                float y = (Y - (Height / 2));
                float rotatedX = (float)(x * Math.Cos(AimAngle) - y * Math.Sin(AimAngle));
                float rotatedY = (float)(x * Math.Sin(AimAngle) + y * Math.Cos(AimAngle));

                return new PointF(rotatedX + X, rotatedY + y);
            }
        }
        public PointF BL
        {
            get
            {

                float x = (X - (Width / 2));
                float y = (Y + (Height / 2));
                float rotatedX = (float)(x * Math.Cos(AimAngle) - y * Math.Sin(AimAngle));
                float rotatedY = (float)(x * Math.Sin(AimAngle) + y * Math.Cos(AimAngle));

                return new PointF(rotatedX + X, rotatedY + y);
            }
        }
        public PointF BR
        {
            get
            {

                float x = (X + (Width / 2));
                float y = (Y + (Height / 2));
                float rotatedX = (float)(x * Math.Cos(AimAngle) - y * Math.Sin(AimAngle));
                float rotatedY = (float)(x * Math.Sin(AimAngle) + y * Math.Cos(AimAngle));

                return new PointF(rotatedX + X, rotatedY + y);
            }
        }


        public virtual void Move_Tick(object sender, EventArgs e)
        {
            CheckProjectileCollisions();
            CheckHits();
            UpdatePosition();
            UpdateVelocity();
        }


        public virtual void WorldTick(object sender, EventArgs e)
        {
            LifeTime++;
        }


        public void EnterWorld(PixelWorld world)
        {
            CurrentWorld = world;
            CurrentWorld.MovedDown += WorldTick;

            _moveTimer.Tick += Move_Tick;
            _moveTimer.Interval = 1;
            _moveTimer.Start();

            if (!world.Entities.Contains(this))
                world.Entities.Add(this);

            X = InitialX;
            Y = InitialY;
        }

        public void UpdatePosition()
        {

            X = X + Velocity.X;
            Y = Y + Velocity.Y;

            if (X < 0)
            {
                X = 0;
                Velocity.X = 0;
            }

            if (X > CurrentWorld.Width)
            {
                X = CurrentWorld.Width;
                Velocity.X = 0;
            }

            if (Y < 0)
            {
                Y = 0;
                Velocity.Y = 0;
            }

            if (Y > CurrentWorld.Height)
            {
                Y = CurrentWorld.Height;
                Velocity.Y = 0;
            }
        }

        public virtual void UpdateVelocity()
        {
            Velocity.AreaDendistyFactor = CurrentWorld.AreaDensityFactor;
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

        public virtual void OnHitRecieved(Projectile projectile)
        {
            HP = HP - projectile.Damage;
            if (HP < 0) 
                HP = 0;
        }


        public void CheckProjectileCollisions()
        {
            if(EnableProjectileCollisions)
            for(int i = 0; i < CurrentWorld.Projectiles.Count; i++)
            {
                Projectile prj = CurrentWorld.Projectiles[i];
                bool hit = false;

                hit = hit || xMath.LineIntersectsLine(TL, TR, prj.PTStart, prj.PTEnd);
                hit = hit || xMath.LineIntersectsLine(TR, BR, prj.PTStart, prj.PTEnd);
                hit = hit || xMath.LineIntersectsLine(BR, BL, prj.PTStart, prj.PTEnd);
                hit = hit || xMath.LineIntersectsLine(BL, TL, prj.PTStart, prj.PTEnd);

                if(hit)
                {
                    HP = HP - prj.Damage;
                    prj.AddHit(1);
                }
            }
        }

        private void CheckHits()
        {
            for (int pr = 0; pr < CurrentWorld.Projectiles.Count; pr++)
            {
                Projectile p = CurrentWorld.Projectiles[pr];
                PointF pt1 = new PointF(p.X, p.Y);
                PointF pt2 = new PointF((p.X + p.Velocity.X), (p.Y + p.Velocity.Y));

                int hits = CurrentWorld.PixelUnits.RemoveAll(pu => xMath.LineIntersectsRect(pt1, pt2, pu.GetRectangle()));

                if (hits > 0)
                {
                    p.AddHit(hits);
                }
            }
        }
    }
}


