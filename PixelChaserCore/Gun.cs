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
    public abstract class Gun: IGun,IDisposable
    {
        public Entity Entity { get; private set; }
        public abstract string Name { get; }
        public float Damage { get; set; } = 1;
        public int Cooldown { get;  set; } = 0;
        private int _cooldownCounter = 0;
        private int _cooldownInterval = 1;
        private Timer _cooldownTimer;
        public float ProjectileSpeed { get;  set; } = 10;
        public float ProjectileLength { get;set; } = 10;
        public float ProjectileWidth { get;  set; } = 1;
        public float Range { get;set; } = 300;
        public int Ammo { get; set; } = -1;

        public virtual float X
        {
            get
            {
                return Endpoint.X;
            }
        }
        public virtual float Y
        {
            get
            {
                return Endpoint.Y;
            }
        }
        public PointF Endpoint
        {
            get
            {
                PointF pt = new PointF();

                double x = Entity.X + (Math.Cos(-Entity.AimAngle + Math.PI / 2) * (Entity.Width / 2 + Offset));
                double y = Entity.Y + (Math.Sin(-Entity.AimAngle + Math.PI / 2) * (Entity.Height / 2 + Offset));

                pt.X = (float)x;
                pt.Y = (float)y;

                return pt;
            }
        }

        public virtual float InitialX { get { return (float)(Entity.X + (Entity.Width / 2 + Offset) * Math.Sin(-Entity.AimAngle)); } }
        public virtual float InitialY { get { return (float)(Entity.Y + (Entity.Height/2+Offset) * Math.Cos(-Entity.AimAngle));  } }
        public virtual float Offset { get; set; } = 2;
        public float AimX { get { return Entity.AimX; } }
        public float AimY { get { return Entity.AimY; } }
        public int Hits { get;  set; } = 0;
        public event EventHandler SuccessHit;
        public bool ReadyToShoot
        {
            get
            {
                return (_cooldownCounter == 0 && !_cooldownTimer.Enabled && Ammo != 0);
            }
        }
        public void LoadEntity(Entity entity)
        {
            Entity = entity;
            _cooldownTimer = new Timer();
            _cooldownTimer.Tick += CooldownCounting;
            _cooldownTimer.Interval = _cooldownInterval;
        }

        private void CooldownCounting(object sender, EventArgs e)
        {
            if (_cooldownCounter < Cooldown)
                _cooldownCounter++;
            else if (_cooldownCounter >= Cooldown)
            {
                _cooldownTimer.Stop();
                _cooldownCounter = 0;
            }
        }

        public virtual void Shoot()
        {
            if (!ReadyToShoot)
                return;

            Projectile bullet = new Projectile(Entity.CurrentWorld,Entity.TypeID);

            double dist = Math.Sqrt(Math.Pow(Entity.X - AimX, 2) + Math.Pow(AimY - Entity.Y, 2));
            double lengthFactor = ProjectileLength / dist;

            //bullet.X = (float)((X + Math.Sin(Entity.AimAngle) * Entity.Width / 2) + Position.X);
            //bullet.Y = (float)((Y + Math.Cos(Entity.AimAngle) * Entity.Height / 2) + Position.Y);

            bullet.X = X;
            bullet.Y = Y;

            bullet.Length = ProjectileLength;
            bullet.Width = ProjectileWidth;

            bullet.Velocity.X = (float)((AimX - InitialX) * lengthFactor);
            bullet.Velocity.Y = (float)((AimY - InitialY) * lengthFactor);

            bullet.Angle = (float)Entity.AimAngle;
            bullet.LifetimeDistance = Range;
            bullet.SourceID = Entity.ID;


            if(Ammo > 0)
                Ammo--;

            Entity.CurrentWorld.Projectiles.Add(bullet);
            ResetCooldown();
        }

        public void ResetCooldown(bool startTimer = true)
        {
            _cooldownTimer.Enabled = startTimer;
            _cooldownCounter = 0;
        }


        protected void OnSuccessHit(Projectile projectile)
        {
            Hits = Hits + projectile.Hits;
            OnHit(projectile);
            if (SuccessHit != null)
                SuccessHit(projectile, new EventArgs());
        }

        public virtual void OnHit(Projectile projectile)
        {
            Range++;
        }

        private void Bullet_SuccessHit(object sender, EventArgs e)
        {
            OnSuccessHit((Projectile)sender);
        }

        // To detect redundant calls
        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                this._cooldownTimer.Dispose();
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }
    }
}
