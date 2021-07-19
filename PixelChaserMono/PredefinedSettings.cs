using System;
using System.Collections.Generic;
using System.Text;

namespace PixelChaser
{
    public partial class Game1
    {

        private void StopPixelGenerator()
        {
            World.Generator.Enabled = false;
        }

        private void StartPixelGenerator()
        {
            World.Generator.Enabled = true;
        }

        private void SetEmptyArea()
        {

            StopPixelGenerator();
            World.ClearPixels();

            Chaser.GunPower = 15;
            KeyDelay = 35;
            Chaser.MoveInterval = 35;
            World.AreaDensityFactor = 1.5f;
            IsFixedTimeStep = true;

        }


    }
}
