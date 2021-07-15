using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelChaser
{
    public class Projectile
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Velocity Velocity { get; set; } = new Velocity();
        public PixelWorld World { get; private set; }

        public int Hits { get; set; } = 0;

        public int MaxHits { get; set; } = 1;
        public bool IsDead
        {
            get
            {
                if (Hits >= MaxHits)
                    return true;

                if (TotalDistance >= LifetimeDistance)
                    return true;

                if (X < 0 || X > World.Width || Y < 0 || Y > World.Height)
                    return true;

                return false;
            }
        }
        public double PowerFactor { get; set; } = 1;

        public double LifetimeDistance { get; set; } = 400;
        public double TotalDistance { get; private set; } = 0;

        public Projectile(PixelWorld world)
        {
            World = world;
            World.MovedDown += Tick;
        }

        private void Tick(object sender, EventArgs e)
        {
            UpdatePosition();
        }

        public Projectile(PixelWorld world ,double x, double y)
        { 
            World = world;
            World.MovedDown += Tick;
            X = x;
            Y = y; 
        }

        public void UpdatePosition()
        {
            X = X + (Velocity.X * PowerFactor);
            Y = Y + (Velocity.Y* PowerFactor);

            double distance = Math.Sqrt(Math.Pow(Velocity.X * PowerFactor, 2)+Math.Pow(Velocity.Y * PowerFactor, 2));

            TotalDistance = TotalDistance + distance;
        }


    }
}
