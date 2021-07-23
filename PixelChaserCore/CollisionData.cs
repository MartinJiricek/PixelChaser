using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace PixelChaser
{
    public enum CollisionShape
    {
        PointList,
        Rectangle,
        Circle,
    }

    public class CollisionData
    {
        public PointF this [int ix]
        {
            get { return Points[ix]; }
         }
        public int Count { get { return Points.Count; } }
        public bool IgnoreRotation { get; set; } = false;
        public bool EnableCollisions { get; set; } = true;
        public List<PointF> Points
        {
            get
            {
                List<PointF> pts = new List<PointF>();

                if (IgnoreRotation)
                    for (int i = 0; i < _originPoints.Count; i++)
                        pts.Add(new PointF(_originPoints[i].X + C.X, _originPoints[i].Y + C.Y));
                else
                {
                    return _rotatedPoints;
                }

                return pts;
            }
        }

        private List<PointF> _originPoints = new List<PointF>();
        private List<PointF> _rotatedPoints = new List<PointF>();

        private Entity _entity;
        
        public PointF C { get { return new PointF(_entity.X, _entity.Y); } }

        public event EventHandler NewProjectileCollision;

        public CollisionData(Entity entity)
        {
            _entity = entity;
            _entity.AimChanged += RotationChanged;
            _entity.PositionChanged += PositionChanged;
            _entity.WorldEntered += WorldEntered;
            SetEntityTriangle();
            
        }

        private void WorldEntered(object sender, EventArgs e)
        {

        }


        private void PositionChanged(object sender, EventArgs e)
        {
            Rotate(_entity.AimAngle);
        }

        private void RotationChanged(object sender, EventArgs e)
        {
            Rotate(_entity.AimAngle);
        }

        public void Clear()
        {
            _originPoints.Clear();
            _rotatedPoints.Clear();
        }

        public void AddPoint(PointF pt)
        {
            _originPoints.Add(pt);
            _rotatedPoints.Add(pt);
        }       

        public void Rotate(double radians)
        {
            for(int i = 0; i < Count; i++)
            {
                RotatePoint(i, radians);
            }
        }

        private void RotatePoint(int ix, double rotation)
        {
            PointF pt = _originPoints[ix];
            _rotatedPoints[ix] = GetRotatedPoint(pt, rotation);
        }

        private PointF GetRotatedPoint(PointF pt, double rotation)
        {
            PointF ptx = new PointF(pt.X + C.X, pt.Y + C.Y);

           double c_dist = Math.Sqrt(Math.Pow(ptx.X - C.X, 2) + Math.Pow(ptx.Y - C.Y, 2));
           double radians = rotation + xMath.GetDefaultAngle(ptx, C);

           float  x = (float)(c_dist * Math.Cos(-radians) + C.X);
           float  y = (float)(c_dist * Math.Sin(-radians) + C.Y);

            return new PointF(x, y);
        }

        public bool CheckProjectileCollision(Projectile projectile)
        {
            if (projectile == null)
                return false;

            PointF pt1 = new PointF(projectile.X, projectile.Y);
            PointF pt2 = new PointF(projectile.X + projectile.Velocity.X, projectile.Y+projectile.Velocity.Y);

            if(CheckLineCollision(pt1, pt2))
            {   
                if (NewProjectileCollision != null)
                    NewProjectileCollision(projectile, new EventArgs());
            }

            return false;
        }

        public bool CheckLineCollision(PointF pt1, PointF pt2)
        {
            for (int i = 0; i < Count; i++)
            {
                int ii = i + 1;
                if (ii >= Count)
                    ii = 0;

                if( xMath.CheckLinesIntersects(Points[i], Points[ii], pt1, pt2) )
                {                    
                    return true;
                }
            }
            return false;
        }

        public void SetEntityRectangle()
        {
            Clear();

            PointF pt1 = new PointF(-_entity.Width / 2, -_entity.Height / 2);
            PointF pt2 = new PointF(_entity.Width / 2, -_entity.Height / 2);
            PointF pt3 = new PointF(_entity.Width / 2, +_entity.Height / 2);
            PointF pt4 = new PointF(-_entity.Width / 2, +_entity.Height / 2);

            AddPoint(pt1);
            AddPoint(pt2);
            AddPoint(pt3);
            AddPoint(pt4);
        }

        public void SetEntityTriangle()
        {
            Clear();

            PointF pt1 = new PointF(0, -_entity.Height / 2);
            PointF pt3 = new PointF(_entity.Width / 2, _entity.Height / 2);
            PointF pt4 = new PointF(-_entity.Width / 2, _entity.Height / 2);

            AddPoint(pt1);
            AddPoint(pt3);
            AddPoint(pt4);
        }


    }
}
