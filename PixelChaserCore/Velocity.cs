using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelChaser
{
    public class Velocity
    {
        public double X
        {
            get
            {
                
                return _x;
            }
            set
            {
                if (value > MaxLimit && !IgnoreLimit)
                    _x = MaxLimit;
                else if (value < (MaxLimit * -1) && !IgnoreLimit)
                    _x = (MaxLimit * -1);
                _x = value;
            }
        }
        private double _x = 0;

        public double Y
        {
            get
            {               
                return _y;
            }
            set
            {
                if (value > MaxLimit && !IgnoreLimit)
                    _y = MaxLimit;
                else if (value < (MaxLimit * -1) && !IgnoreLimit)
                    _y = (MaxLimit * -1);
                _y = value;
            }
        }
        private double _y = 0;
        public double MaxLimit { get; set; } = 0;
        public bool IgnoreLimit { get { return MaxLimit == 0; } }

        public Velocity()
        {

        }
    }
}
