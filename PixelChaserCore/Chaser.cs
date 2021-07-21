using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
    

namespace PixelChaser
{
    public class Chaser:Entity
    {
        public override string Name { get { return "Chaser"; } }
        public override string TypeID { get { return "ChaserDefault"; } }
        public override float InitialX { get { if (CurrentWorld == null) return Width; else return CurrentWorld.Width / 2; } }
        public override float InitialY { get { if (CurrentWorld == null) return Height; else return CurrentWorld.Height / 2; } }

        public Chaser()
        {
            //Velocity.MaxLimit = 0;

            Arsenal.Clear();
            Arsenal.Add(new SelfUpgraderGun(this));
            
            
            Width = 64;
            Height = 64;
            HP = 100;
            Gun.Range = 1000;
            Gun.Cooldown = 5;
            Gun.ProjectileLength = 5;
            Gun.ProjectileWidth = 5;
            Speed = 1.4f;
            SelectedGun = 0;          
        }

        private bool _velocitySwitch = false;
        public override void Move_Tick(object sender, EventArgs e)
        {
            base.Move_Tick(sender, e);

            if (_velocitySwitch)
            {

                if (Velocity.X > 0)
                {
                    Velocity.X = Velocity.X - CurrentWorld.AreaDensityFactor;
                    _velocitySwitch = false;
                }
                else if (Velocity.X < 0)
                {
                    Velocity.X  = Velocity.X + CurrentWorld.AreaDensityFactor;
                    _velocitySwitch = false;
                }

                if (Velocity.Y > 0)
                {
                    Velocity.Y = Velocity.Y - CurrentWorld.AreaDensityFactor;
                    _velocitySwitch = false;
                }
                else if (Velocity.Y < 0)
                {
                    Velocity.Y = Velocity.Y + CurrentWorld.AreaDensityFactor;
                    _velocitySwitch = false;
                }
            }
            else _velocitySwitch = true;

        }




    }
}
