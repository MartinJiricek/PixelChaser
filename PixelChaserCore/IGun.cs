using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace PixelChaser
{
    public interface IGun
    {
        string Name { get; } 
        float Damage { get; set; }
        int Cooldown { get;  }
        int Hits { get; }
        float ProjectileSpeed { get;}
        float ProjectileLength { get;}
        float ProjectileWidth { get; }
        float Range { get; }
        int Ammo { get;  }
        float InitialX { get;  }
        float InitialY { get;  }
        float AimX { get;  }
        float AimY { get;  }
        bool ReadyToShoot { get; }

        void Shoot();
       
    }
}
