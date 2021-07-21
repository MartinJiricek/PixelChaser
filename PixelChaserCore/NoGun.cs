using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelChaser
{
    public class NoGun:Gun
    {
        public override string Name { get { return "NoGun"; } }

        public NoGun(Entity entity)
        {
            LoadEntity(entity);
        }
    }
}
