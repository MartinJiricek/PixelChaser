using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Shapes;
using System;
namespace PixelChaser
{
    public partial class Game1
    {
        private void InputProcessing()
        {

            if (KeysNow.IsKeyDown(Keys.Escape))
                Exit();

            bool _moved = false;
            if (KeysNow.IsKeyDown(Keys.W) && !_keyDelayTimer.Enabled)
            {
                Chaser.MoveUp();
                _moved = true;
                // _keyDelayTimer.Start();
            }

            if (KeysNow.IsKeyDown(Keys.A) && !_keyDelayTimer.Enabled)
            {
                Chaser.MoveLeft();
                _moved = true;
                // _keyDelayTimer.Start();
            }

            if (KeysNow.IsKeyDown(Keys.S) && !_keyDelayTimer.Enabled)
            {
                Chaser.MoveDown();
                _moved = true;
                // _keyDelayTimer.Start();
            }

            if (KeysNow.IsKeyDown(Keys.D) && !_keyDelayTimer.Enabled)
            {
                Chaser.MoveRight();
                _moved = true;
                // _keyDelayTimer.Start();
            }

            if (_moved)
            {
                _keyDelayTimer.Start();
                _moved = false;

            }

            if (KeysNow.IsKeyDown(Keys.Space))
            {
                World.MoveDown();

                if (Chaser.Velocity.X > 0)
                    Chaser.Velocity.X = Chaser.Velocity.X - 1;
                else if (Chaser.Velocity.X < 0)
                    Chaser.Velocity.X = Chaser.Velocity.X + 1;


                if (Chaser.Velocity.Y > 0)
                    Chaser.Velocity.Y = Chaser.Velocity.Y - 1;
                else if (Chaser.Velocity.Y < 0)
                    Chaser.Velocity.Y = Chaser.Velocity.Y + 1;
            }

            if (MouseNow.LeftButton == ButtonState.Pressed)
            {
                OnMouseClick();
            }

            if (KeysNow.IsKeyDown(Keys.F1))
            {
                DebugWindow win = new DebugWindow(this);
                win.Show();
            }


            if (KeysNow.IsKeyDown(Keys.F2))
                SetEmptyArea();

                _keysBefore = Keyboard.GetState();
        }

    }
}
