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
        public override string Name { get { return "chaser"; } }
        public override string TypeName { get { return "ChaserDefault"; } }
        public override string TextureID { get { return "Chaser2"; } }

        public override float InitialX { get { if (World == null) return Width; else return World.Width / 2; } }
        public override float InitialY { get { if (World == null) return Height; else return World.Height / 2; } }

        public Chaser()
        {
            //Velocity.MaxLimit = 0;

            Arsenal.Clear();
            Arsenal.Add(new SelfUpgraderGun(this));

            Width  = 32;
            Height = 32;
            HP = 1000;
            Gun.Range = 1000;
            Gun.Cooldown = 20;
            Gun.ProjectileLength = 50;
            Gun.ProjectileWidth = 1f;
            Gun.ProjectileSpeed = 0.5f;
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
                    Velocity.X = Velocity.X - World.AreaDensityFactor;
                    _velocitySwitch = false;
                }
                else if (Velocity.X < 0)
                {
                    Velocity.X  = Velocity.X + World.AreaDensityFactor;
                    _velocitySwitch = false;
                }

                if (Velocity.Y > 0)
                {
                    Velocity.Y = Velocity.Y - World.AreaDensityFactor;
                    _velocitySwitch = false;
                }
                else if (Velocity.Y < 0)
                {
                    Velocity.Y = Velocity.Y + World.AreaDensityFactor;
                    _velocitySwitch = false;
                }
            }
            else _velocitySwitch = true;

        }

        public override void EnterWorld(PixelWorld world)
        {
            base.EnterWorld(world);
            world.MovedDown += HPCheck;
            world.SetChaserEntity(this);
        }

        public event EventHandler ChaserDied;
        private void HPCheck(object sender, EventArgs e)
        {
            if (HP <= 0 && ChaserDied != null)
                ChaserDied(this, new EventArgs());
        }
    }
}
