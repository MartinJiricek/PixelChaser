using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PixelChaser
{
    public class SpatialSector
    {

        public string this[int ix]
        {
            get { return Objects[ix]; }
        }
        public string ID { get; private set; }

        public float X { get; set; }
        public float Y { get; set; }

        public List<string> Objects { get; private set; } = new List<string>();

        public SpatialSector(float x, float y) { X = x; Y = y; ID = $"{x}-{y}"; }

        public void Clear()
        {
            Objects.Clear();
        }

        public bool Contains(string objectID)
        {
            return Objects.Contains(objectID);
        }

        public void Add(string objectID)
        {
            if (Contains(objectID))
                return;

            Objects.Add(objectID);

        }




    }
}
