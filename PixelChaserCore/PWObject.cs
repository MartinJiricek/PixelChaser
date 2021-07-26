using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PixelChaser
{
    public abstract class PWObject:IObject
    {
        public string ID
        {
            get
            {
                if (_id == "")
                    _id = Guid.NewGuid().ToString();

                return _id;
            }
        }
        private string _id = "";
        public virtual string Name { get; protected set; }
        public virtual string TypeName { get { return this.GetType().Name; }   }
        public string SourceID { get; set; }
        private float _x = 0;
        private float _y = 0;
        public float X
        {
            get { return _x; }
            set
            {
                if (value != _x)
                {
                    _x = value;
                    OnPositionChanged();
                }
            }


        }
        public float Y
        {
            get { return _y; }
            set
            {
                if (value != _y)
                {
                    _y = value;
                    OnPositionChanged();
                }
            }
        }
        public Velocity Velocity { get; set; } = new Velocity();
        public CollisionData CollisionData { get; protected set; }
        public PixelWorld World { get; protected set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public virtual double Angle { get; set; }

        public event EventHandler PositionChanged;
        public event EventHandler AngleChanged;
        public event EventHandler WorldEntered;

        protected virtual void OnPositionChanged()
        {
            if (PositionChanged != null)
                PositionChanged(this, new EventArgs());
        }
        protected virtual void OnAngleChanged()
        {
            if (AngleChanged != null)
                AngleChanged(this, new EventArgs());
        }

        protected virtual void OnWorldEntered()
        {
            if (WorldEntered != null)
                WorldEntered(this, new EventArgs());
        }

        public void LoadWorld(PixelWorld world)
        {
            World = world;
            World.MovedDown += Tick;
            World.AddPWObject(this);
            OnWorldEntered();
        }

        private void Tick(object sender, EventArgs e)
        {
            UpdatePosition();
        }


        protected virtual void UpdatePosition()
        {
            X = X + (Velocity.X);
            Y = Y + (Velocity.Y);
        }

        public float GetDistanceFrom(PointF pt)
        {
            return (float)Math.Sqrt(Math.Pow(pt.X - X, 2) + Math.Pow(pt.Y - Y, 2));
        }




    }



}

