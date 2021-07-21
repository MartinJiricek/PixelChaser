using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PixelChaser
{
    public class Projectile
    {
        public string SourceID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
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
        public float PowerFactor { get; set; } = 1;
        public float Length { get; set; } = 10;
        public float Width { get; set; } = 3;
        public int Damage { get; set; } = 1;
        public double LifetimeDistance { get; set; } = 400;
        public double TotalDistance { get; private set; } = 0;

        public float Angle { get; set; }

        public event EventHandler SuccessHit;

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
            LoadWorld(world);
        }

        public void LoadWorld(PixelWorld world)
        {
            World = world;
            World.MovedDown += Tick;
            Velocity.AreaDendistyFactor = World.AreaDensityFactor;

        }

        private void Tick(object sender, EventArgs e)
        {
            UpdatePosition();
        }

        public Projectile(PixelWorld world ,float x, float y)
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

        public void AddHit(int hitCount = 1)
        {
            if (hitCount > 0)
            {
                Hits = Hits + hitCount;
                if (SuccessHit != null) SuccessHit(this, new EventArgs());
            }
        }


    }
}
