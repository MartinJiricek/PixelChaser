using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PixelChaser
{
    public abstract class Gun:IGun
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
        public float X { get { return Entity.X; } }
        public float Y { get { return Entity.Y; } }
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

            double dist = Math.Sqrt(Math.Pow(Entity.X - AimX, 2) + Math.Pow(AimY - Y, 2));
            double lengthFactor = ProjectileLength / dist;

            bullet.X = (float)( X + Math.Sin(Entity.AimAngle) * Entity.Height / 2);
            bullet.Y = (float)( Y + Math.Cos(Entity.AimAngle) * Entity.Height / 2);

            bullet.Length = ProjectileLength;
            bullet.Width = ProjectileWidth;
            bullet.Velocity.X = (float)((AimX - X) * lengthFactor);
            bullet.Velocity.Y = (float)((AimY - Y) * lengthFactor);

            bullet.Angle = (float)Entity.AimAngle;
            bullet.LifetimeDistance = Range;

            bullet.SuccessHit += Bullet_SuccessHit;

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
    }
}
