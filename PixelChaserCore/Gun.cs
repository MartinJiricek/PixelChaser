using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PixelChaser
{
    public class Gun
    {
        public Chaser Chaser { get;private set; }
        public string Name { get; set; } = "Generic";
        public float Damage { get; set; }
        public int Cooldown { get; private set; } = 10;
        private int _cooldownCounter = 0;
        private int _cooldownInterval = 1;
        private Timer _cooldownTimer;
        public float ProjectileSpeed { get; private set; } = 10;

        public float ProjectileLength { get; private set; } = 10;
        public float ProjectileWidth { get; private set; } = 1;
        public float Range { get; private set; } = 300;
        public int Ammo { get; set; } =-1;
        public float X { get { return Chaser.X; } }
        public float Y { get { return Chaser.Y; } }
        public float AimX { get { return Chaser.AimX; } }
        public float AimY { get { return Chaser.AimY; } }


        public bool ReadyToShoot
        {
            get
            {
                return (_cooldownCounter == 0 && !_cooldownTimer.Enabled && Ammo != 0);
            }
        }

        public Gun() { }
        public Gun(Chaser chaser) { LoadChaser(chaser); }

        public void LoadChaser(Chaser chaser)
        {
            Chaser = chaser;
            _cooldownTimer = new Timer();
            _cooldownTimer.Tick += CooldownCounting;
            _cooldownTimer.Interval = _cooldownInterval;
        }

        private void CooldownCounting(object sender, EventArgs e)
        {
           if(_cooldownCounter < Cooldown)
            _cooldownCounter++;
           else if (_cooldownCounter >= Cooldown)
            {
                _cooldownTimer.Stop();
                _cooldownCounter = 0;
            }
        }

        public void Shoot()
        {
            if (!ReadyToShoot)
                return;

            Projectile bullet = new Projectile(Chaser.CurrentWorld);

            bullet.X = X;
            bullet.Y = Y;

            double dist = Math.Sqrt(Math.Pow(Chaser.X - AimX, 2) + Math.Pow(AimY - Y, 2));
            double lengthFactor = ProjectileLength / dist;

            bullet.Length = ProjectileLength;
            bullet.Width = ProjectileWidth;
            bullet.Velocity.X = (float)((AimX - X) * lengthFactor);
            bullet.Velocity.Y = (float)((AimY - Y) * lengthFactor);


            bullet.Angle = (float)Chaser.AimAngle;
            bullet.LifetimeDistance = Range;

            Ammo--;
            Chaser.CurrentWorld.Projectiles.Add(bullet);
            _cooldownCounter = 0;
            _cooldownTimer.Start();



        }
    }
}
