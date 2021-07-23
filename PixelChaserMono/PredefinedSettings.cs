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

            KeyDelay = 35;
            Chaser.MoveInterval = 35;
            World.AreaDensityFactor =1.1f;
            World.Generator.TopSpawn = false;
            World.Generator.BottomSpawn = false;
            World.Generator.LeftSpawn = false;
            World.Generator.RightSpawn = true;
            World.Generator.MaxUnitsOnTop = 0;
            World.Generator.MaxUnitsOnRight = 0;
            World.Generator.MaxUnitsOnBottom = 0;
            World.Generator.MaxUnitsOnLeft = 0;
            World.Generator.MaxBatchSize = 0;
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            DrawAllAimingLines = false;

            Enemy enemy = new Enemy();
            enemy.EnterWorld(World);

            StartPixelGenerator();

        }


    }
}
