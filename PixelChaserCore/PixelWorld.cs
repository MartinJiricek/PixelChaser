using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;

namespace PixelChaser
{
    public class PixelWorld
    {
        public string Name { get; set; } = "default";
        public List<PixelUnit> PixelUnits { get; private set; } = new List<PixelUnit>();
        public List<Projectile> Projectiles { get; private set; } = new List<Projectile>();

        public event EventHandler MovedDown;

        public PixelGenerator Generator { get; private set; }

        private RandomGenerator _rdm = new RandomGenerator();

        public int MoveCounter { get; private set; } = 0;

        public int Width { get; private set; } = 50;
        public int Height { get;private  set; } = 50;
        public bool Locked { get; set; }

        public Point Cursor { get; private set; }

        public int MaxUnits { get; set; }

        public int MaxPixelSize { get; set; } = 16;
        public double MaxPixelSpeed { get; set; } = 10;
        public int MaxUnitsPerRow { get; set; } = 20;

        private double _areaDensityFactor = 10;
        private double _densityHysteresis = 2;
        public double AreaDensityFactor
        {
            get { return _rdm.Next((int)(_areaDensityFactor-DensityHysteresis),(int)( _areaDensityFactor + DensityHysteresis)); }
            set
            {
                if (value < 0)
                    _areaDensityFactor = 0;
                else
                    _areaDensityFactor = value;
            }
        }
        public double DensityHysteresis
        {
            get { return _densityHysteresis; }
            set
            {
                if (value < 0)
                    _densityHysteresis = 0;
                else
                    _densityHysteresis = value;
            }
        }

        public List<int> AvailablePixelUnitTypes { get; private set; } = new List<int>() { 0,1,2,3 };

        public PixelWorld(int width, int height)
        {
            Width = width;
            Height = height;
            Generator = new PixelGenerator(this);
        }

        public void MoveDown()
        {
            if (MovedDown != null) 
                MovedDown(this, new EventArgs());

            MoveCounter++;
            CleanupObjects();

        }


        public void CleanupObjects()
        {
            if (PixelUnits.Count > 0)
                try
                {
                    PixelUnits.RemoveAll(pu => pu.Y >= Height || pu.Y < 0 || pu.X >= Width || pu.X < 0);
                    Projectiles.RemoveAll(p => p.IsDead);
                }
                catch { }
        }

    }
}
