using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelChaser
{
    public class Velocity
    {
        public float X
        {
            get
            {
                
                return _x * AreaDendistyFactor;
            }
            set
            {
                if (value > MaxLimit && !IgnoreLimit)
                    _x = (float)MaxLimit;
                else if (value < (MaxLimit * -1) && !IgnoreLimit)
                    _x = (float)(MaxLimit * -1);
                _x = value;
            }
        }
        private float _x = 0;
        public float Y
        {
            get
            {               
                return _y * AreaDendistyFactor;
            }
            set
            {
                if (value > MaxLimit && !IgnoreLimit)
                    _y = (float)MaxLimit;
                else if (value < (MaxLimit * -1) && !IgnoreLimit)
                    _y = (float)(MaxLimit * -1);
                _y = value;
            }
        }
        private float _y = 0;
        public double MaxLimit { get; set; } = 0;
        public bool IgnoreLimit { get { return MaxLimit == 0; } }
        public float AreaDendistyFactor { get; set; } = 1;
        public Velocity()
        {

        }
    }
}
