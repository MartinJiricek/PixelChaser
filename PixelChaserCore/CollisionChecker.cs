using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PixelChaser
{
    public class CollisionChecker
    {
        public Dictionary<string, SpatialSector> Sectors { get; private set; } = new Dictionary<string, SpatialSector>();

        private PixelWorld _world;

        public int Rows { get; set; } = 32;
        public int Cols { get; set; } = 32;
        public int Count { get { return Rows * Cols; } }

        public float SectorWidth { get { return (float)(_world.Width / Cols); } }
        public float SectorHeight { get { return (float)(_world.Height / Rows); } }


        public CollisionChecker(PixelWorld world)
        {
            _world = world;
            CreateSectors();
        }

        private void CreateSectors()
        {
            for (int c = 0; c < Cols; c++)
                for (int r = 0; r < Rows; r++)
                {
                    SpatialSector sector = new SpatialSector(c, r);

                    Sectors.Add(sector.ID, sector);
                }
        }






    }
}
