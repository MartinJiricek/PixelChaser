using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PixelChaser
{
    public class Projectile :PWObject,ICollision,IObject
    {
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
        public float PowerFactor { get; set; } = 1;
        public int Damage { get; set; } = 1;
        public double LifetimeDistance { get; set; } = 400;
        public double TotalDistance { get; private set; } = 0;

        public List<string> EndTargets { get; private set; } = new List<string>();


        public PointF PTStart
        {
            get
            {
                return new PointF(X, Y);
            }
        }
        public PointF PTEnd
        {
            get
            {
                return new PointF(X+Velocity.X, Y+Velocity.Y);
            }
        }

        public Projectile(PixelWorld world, string sourceID)
        {
            SourceID = SourceID;
            CollisionData = new CollisionData(this);
            LoadWorld(world);
        }
        private void Tick(object sender, EventArgs e)
        {
            UpdatePosition();
        }

        public Projectile(PixelWorld world ,float x, float y)
        {
            LoadWorld(world);

            X = x;
            Y = y; 
        }





    }
}
