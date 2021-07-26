using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelChaser
{
    public interface IObject
    {
        float X { get; set; }
        float Y { get; set; }
        Velocity Velocity { get; }
        PixelWorld World { get; }
        float Width { get;  }
        float Height { get; }
        double Angle { get;  }
        string ID { get; }
        string TypeName { get;}
        string Name { get; }

        event EventHandler PositionChanged;
        event EventHandler AngleChanged;
        event EventHandler WorldEntered;
    }
}
