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
        public double X
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
        public double Y
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
        private double _x = 0;
        private double _y = 0;

        public Point Position
        {
            get
            {
                return new Point((int)X, (int)Y);
            }
            set
            {
                _x = value.X;
                _y = value.Y;
            }
        }

        public int TypeID { get; set; }

        public Velocity Velocity { get; set; } = new Velocity();

        private int _a = 255;
        private int _r = 255;
        private int _g = 0;
        private int _b = 0;

        public int A
        {
            get { return _a; }
            set
            {
                if (value < 0)
                    _a = 0;
                else if (value > 255)
                    _a = 255;
                else
                    _a = value;
            }
        }
        public int R
        {
            get { return _r; }
            set
            {
                if (value < 0)
                    _r = 0;
                else if (value > 255)
                    _r = 255;
                else
                    _r = value;
            }
        }
        public int G
        {
            get { return _g; }
            set
            {
                if (value < 0)
                    _g = 0;
                else if (value > 255)
                    _g = 255;
                else
                    _g = value;
            }
        }
        public int B
        {
            get { return _b; }
            set
            {
                if (value < 0)
                    _b = 0;
                else if (value > 255)
                    _b = 255;
                else
                    _a = value;
            }
        }

        public Color MainColor
        {
            get
            {
                return Color.FromArgb(A, R, G, B);
            }
            set
            {
                _a = value.A;
                _r = value.R;
                _g = value.G;
                _b = value.B;
            }
        }

        private int _width = 1;
        private int _height = 1;
        public int Width
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
        public int Height
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

        public Size Size
        {
            get { return new Size(Width, Height); }
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

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)X, (int)Y, Width, Height);
        }

    }
}
