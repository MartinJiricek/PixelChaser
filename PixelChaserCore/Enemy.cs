using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PixelChaser
{
    public class Enemy:Entity
    {
        public override string Name { get { return "Enemy"; } }
        public override string TypeName { get { return "EnemyBasic"; } }
        public override string TextureID { get { return "Enemy1"; } }
        public override float InitialX
        {
            get
            {

                switch (SideID)
                {
                    case 0:
                        return _rdm.Next((int)(-Width / 2), World.Width + (int)(Width / 2));
                    case 1:
                        return  World.Width + (int)(Width / 2);
                    case 2:
                        return  _rdm.Next((int)(-Width / 2), World.Width + (int)(Width / 2));
                    case 3:
                        return -(int)(Width / 2);
                }
                return 0;
            }
        }
        public override float InitialY
        {
            get
            {
                switch (SideID)
                {

                    case 0:
                        return -Height / 2;
                    case 1:
                        return _rdm.Next((int)(-Height / 2), World.Height + (int)(Height / 2));
                       
                    case 2:
                        return Height / 2 + World.Height;
                    case 3:
                        return _rdm.Next((int)(-Height / 2), World.Height + (int)(Height / 2));
                }
                return 0;
            }
        }

        public int SideID
        {
            get
            {
                if (_sideID > 3 || _sideID < 0)
                    _sideID = _rdm.Next(0, 3);

                return _sideID ;
            }
        }

        private int _sideID = -1;

        private RandomGenerator _rdm = new RandomGenerator();
        private int _directionCooldownCounter = 0;
        private int _aimCooldownCounter = 0;
        private int _shootCooldownCounter = 0;
        public int DirectionChangeInterval { get; set; } = 50;
        public int AimChangeInterval { get; set; } = 3;
        public int ShootInterval { get; set; } = 20;


        public Enemy()
        {
            Arsenal.Clear();
            Arsenal.Add(new SelfUpgraderGun(this));

            Width = 32;
            Height = 32;
            HP = 5;
            SelectedGun = 0;
        }

        public override void Move_Tick(object sender, EventArgs e)
        {
            SetRandomAim();
            SetRandomVelocity();
            ShootOnTarget();

            base.Move_Tick(sender, e);
            _directionCooldownCounter++;
            _aimCooldownCounter++;
            _shootCooldownCounter++;
        }
        private void SetRandomVelocity()
        {
            if (_directionCooldownCounter == DirectionChangeInterval)
            {
                Velocity.X = _rdm.Next(-5, 5);
                Velocity.Y = _rdm.Next(-5, 5);
                _directionCooldownCounter = 0;
            }

        }

        private void SetRandomAim()
        {
            if (_aimCooldownCounter == AimChangeInterval)
            {
                AimX = World.Chaser.X;
                AimY = World.Chaser.Y;
                _aimCooldownCounter = 0;
            }
        }

        private void ShootOnTarget()
        {
            if (_shootCooldownCounter == ShootInterval)
            {
                Gun.Shoot();
                _shootCooldownCounter = 0;
               
            }
        }


        private void SetRandomPostion()
        {
            int side = _rdm.Next(0, 3);
            float x = 0;
            float y = 0;

            switch(side)
            {
                case 0:
                    y = -Height / 2;
                    x = _rdm.Next((int)(-Width / 2), World.Width + (int)(Width / 2));
                    break;
                case 1:
                    y = _rdm.Next((int)(-Height / 2), World.Height + (int)(Height/ 2));
                    x = World.Width + (int)(Width / 2);
                    break;
                case 2:
                    y = Height / 2 + World.Height;
                    x = _rdm.Next((int)(-Width / 2), World.Width + (int)(Width / 2));
                    break;
                case 3:
                    y = _rdm.Next((int)(-Height / 2), World.Height + (int)(Height / 2));
                    x = - (int)(Width / 2);
                    break;
            }


            
        }




    }
}


