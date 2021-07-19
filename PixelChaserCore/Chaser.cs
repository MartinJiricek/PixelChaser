using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
    

namespace PixelChaser
{
    public class Chaser
    {
        public int Hits { get; set; } = 0;
        public PixelWorld CurrentWorld { get; private set; }
        public float X { get;private set; }
        public float Y { get; private set; }
        public double RangeDistance { get; set; } = 3;
        public double RangeWidth { get; set; } = 5;
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
        public double MinRange { get; set; } = 10;
        public float AimX { get; set; }
        public float AimY { get; set; }
        public float Width { get; set; } = 32;
        public float Height { get; set; } = 32;

        public double GunPower { get; set; } = 10;
        public Point RangeEndpoint
        {
            get
            {
                Point pt = new Point();

                double x = X + (Math.Cos(AimAngle) * RangeDistance);
                double y = Y + (Math.Sin(AimAngle) * RangeDistance);

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
                if(value > 1 && value < 5000)
                _moveTimer.Interval = value;
            }
        }

        private Timer _moveTimer = new Timer();

        public Gun Gun { get; set; }

        public PixelContainer Container { get; set; } = new PixelContainer();

        public Velocity Velocity { get; set; } = new Velocity();
        public float AirFactor { get; set; } = 3;

        public int GunCooldown { get; set; } = 5;
        private int _gunCooldownCounter = 0; 




        public Chaser()
        {
            Velocity.MaxLimit = 10;
            _moveTimer.Tick += Move_Tick;
            _moveTimer.Interval = 20;

            Gun = new Gun(this);
          
        }

        private void Move_Tick(object sender, EventArgs e)
        {
            UpdatePosition();
        }



        public void ExtractPixels( )
        {
            int count = CurrentWorld.PixelUnits.RemoveAll(pu => CheckIsInRange(pu.X,pu.Y,pu.Width,pu.Height));

          
            Container.AddPixels( count);
            RangeDistance = RangeDistance + count;

         }
        public void EnterWorld(PixelWorld world)
        {
            CurrentWorld = world;
            CurrentWorld.MovedDown += CurrentWorld_MovedDown;

            if (!_moveTimer.Enabled)
                _moveTimer.Start();

            X = world.Width / 2;
            Y = world.Height / 2;
        }

        private void CurrentWorld_MovedDown(object sender, EventArgs e)
        {
            UpdateVelocity();
            CheckHits();

            if (_gunCooldownCounter > 0)
                _gunCooldownCounter --;
        }

        private void UpdatePosition()
        {

            X = X + (int)(Velocity.X);
            Y = Y + (int)(Velocity.Y);

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

        private void UpdateVelocity()
        {
            Velocity.AreaDendistyFactor = CurrentWorld.AreaDensityFactor;

            //if (Velocity.X < 0)
            //    Velocity.X = Velocity.X;
            //else if (Velocity.X > 0)
            //    Velocity.X = Velocity.X;


            //if (Velocity.Y < 0)
            //    Velocity.Y = Velocity.Y;
            //else if (Velocity.Y > 0)
            //    Velocity.Y = Velocity.Y;
        }

        public void MoveUp(float power = 1)
        {
            if (Velocity.Y > 0)
                power = power + AirFactor;

            Velocity.Y = Velocity.Y - power;
        }
        public void MoveDown(float power = 1)
        {
            if (Velocity.Y < 0)
                power = power + AirFactor;

            Velocity.Y = Velocity.Y + power;
        }
        public void MoveLeft(float power = 1)
        {
            if (Velocity.X > 0)
                power = power + AirFactor;

            Velocity.X = Velocity.X - power;
        }
        public void MoveRight(float power = 1)
        {
            if (Velocity.X < 0)
                power = power + AirFactor;


            Velocity.X = Velocity.X + power;
        }
        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool CheckIsInRange(double x,double  y, double w, double  h)
        {
            for (int dx = 0; dx < w; dx++)
                for (int dy = 0; dy < h; dy++)                
                    if (CheckIsInRange(x + dx, y + dy))
                        return true;
                
            return false;
        }

        public bool CheckIsInRange(double  x, double y)
        {
            bool isInRange = false;

            double Ax = X;
            double Ay = Y;
            double Bx = RangeEndpoint.X;
            double By = RangeEndpoint.Y;
            double Cx = x;
            double Cy = y;

        //  double ab = Math.Sqrt(Math.Pow(Bx - Ax, 2) + Math.Pow(By - Ay, 2));
            double bc = Math.Sqrt(Math.Pow(Cx - Bx, 2) + Math.Pow(Cy - By, 2));
            double ac = Math.Sqrt(Math.Pow(Cx - Ax, 2) + Math.Pow(Cy - Ay, 2));

            double sinAlpha = bc / ac;

            double alt = sinAlpha * ac;

            isInRange = alt <= RangeWidth;

            //ABC triangle altitude is lower then range => is in range, returns TRUE

            return isInRange;
        }

        private void CheckHits()
        {
            for (int pr = 0; pr < CurrentWorld.Projectiles.Count; pr++)
            {
                Projectile p = CurrentWorld.Projectiles[pr];
                Point pt1 = new Point((int)p.X, (int)p.Y);
                Point pt2 = new Point((int)(p.X + p.Velocity.X), (int)(p.Y + p.Velocity.Y));

                int hits = CurrentWorld.PixelUnits.RemoveAll(pu => LineIntersectsRect(pt1, pt2, pu.GetRectangle()));

                if (hits > 0)
                {
                    p.Hits = hits;
                }

                Container.AddPixels(hits);
            }

        }

        public static bool LineIntersectsRect(Point p1, Point p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y + r.Height), new Point(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X, r.Y + r.Height), new Point(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }
        private static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }

    }
}
