using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelChaser
{
    public class PixelContainer
    {

        public int TotalPixels { get; private set; } = 0;
        public int Capacity { get; set; } = 100;

        public int Red { get; set; } = 0;
        public int Green { get; set; } = 0;
        public int Blue { get; set; } = 0;

        public bool IsFull { get { return Capacity <= TotalPixels; } }
        public bool IsEmpty { get { return TotalPixels == 0; } }

        public PixelContainer()
        { 
        
        
        }

        public void AddPixels(int count)
        {
            TotalPixels = TotalPixels + count;
        }

        public void AddPixels(IEnumerable<PixelUnit> pixels)
        {
            foreach(PixelUnit pu in pixels)
            {
                Red = Red + pu.R;
                Green = Green + pu.G;
                Blue = Blue + pu.B;
            }
        }







    }
}
