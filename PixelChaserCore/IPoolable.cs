using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelChaser
{
    public interface IPoolable
    {

        void Initialize();
        void Release();

        bool PoolIsValid { get; set; }
        bool PoolIsFree { get; set; }
    }
}
