using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PixelChaser
{
    public partial class Game1
    {
        public PixelWorld World { get; private set; }
        public Chaser Chaser { get; private set; } = new Chaser();

        public int WorldTickInterval { get { return _worldTimer.Interval; } }

        public List<string> UnitTextureNames
        {
            get
            {
                List<string> names = new List<string>();

                names.Add("BasicUnit_Red_1");
                names.Add("BasicUnit_Red_2");
                names.Add("BasicUnit_Red_3");
                names.Add("BasicUnit_Red_4");

                return names;
                     
            }
        }

        private List<Texture2D> _units = new List<Texture2D>();
        public void LoadWorld(PixelWorld world)
        {
            LoadUnitTextures();

            World = world;
            World.MaxUnits = 2000;

            _worldTimer = new System.Windows.Forms.Timer();
            _worldTimer.Interval = 60;
            _worldTimer.Tick += World_Tick;
            _worldTimer.Start();

            Chaser.EnterWorld(World);
        }

        public void SetWorldTickInterval(int interval)
        {
            if (interval > 0 && interval < 5000)
                _worldTimer.Interval = interval;
        }

       


        private void World_Tick(object sender, System.EventArgs e)
        {
            World.MoveDown();
        }

        private void LoadUnitTextures()
        {
            foreach(string name in UnitTextureNames)
            {                
                _units.Add(Content.Load<Texture2D>(name));
            }    

            
        }



    }
}
