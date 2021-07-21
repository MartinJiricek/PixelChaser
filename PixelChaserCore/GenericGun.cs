using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PixelChaser
{
    public class GenericGun:Gun
    {
        public override string Name { get { return "Generic Gun"; } }
        public GenericGun(Chaser chaser) { LoadEntity(chaser); }

        public override void OnHit(Projectile projectile)
        {
            Range++;
        }

        private void Bullet_SuccessHit(object sender, EventArgs e)
        {
            OnSuccessHit((Projectile)sender);
        }
    }
}
