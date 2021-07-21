using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace PixelChaser
{
    public class PixelUnit
    {
        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        private float _x = 0;
        private float _y = 0;

        public PointF Position
        {
            get
            {
                return new PointF(X, Y);
            }
            set
            {
                _x = value.X;
                _y = value.Y;
            }
        }

        public int TypeID { get; set; }

        public Velocity Velocity { get; set; } = new Velocity();


        private float _width = 1;
        private float _height = 1;
        public float Width
        {
            get { return _width; }
            set
            {
                if (value < 1)
                    _width = 1;
                else
                    _width = value;
            }
        }
        public float Height
        {


            get { return _height; }
            set
            {
                if (value < 1)
                    _height = 1;
                else
                    _height = value;
            }
        }

        public SizeF Size
        {
            get { return new SizeF(Width, Height); }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public PixelUnit(PixelWorld world) { world.MovedDown += World_MovedDown; }

        private void World_MovedDown(object sender, EventArgs e)
        {
            MoveDown();
        }

        private void MoveDown()
        {

            X = X + Velocity.X;
            Y = Y + Velocity.Y;

        }

        public RectangleF GetRectangle()
        {
            return new RectangleF(X-Width/2, Y-Height/2, Width, Height);
        }

    }
}
