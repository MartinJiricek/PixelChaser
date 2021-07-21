using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PixelChaser
{
    public abstract class Enemy:Entity
    {
        public override string Name { get { return "EnemyBasic"; } }
        public override string TypeID { get { return "EnemyBasic"; } }
        public override float InitialX { get { if (CurrentWorld == null) return Width; else return CurrentWorld.Width / 4; } }
        public override float InitialY { get { if (CurrentWorld == null) return Height; else return CurrentWorld.Height / 4; } }

        public Enemy()
        {

            Arsenal.Clear();
            Arsenal.Add(new SelfUpgraderGun(this));

            Width = 64;
            Height = 64;
            HP = 100;
            Gun.Range = 1000;
            Gun.Cooldown = 5;
            Gun.ProjectileLength = 5;
            Gun.ProjectileWidth = 5;


            SelectedGun = 0;
        }

        public override void Move_Tick(object sender, EventArgs e)
        {
            base.Move_Tick(sender, e);

        }






    }
}


