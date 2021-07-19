using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Shapes;
using System;

namespace PixelChaser
{
    public static class ChaserExtensions
    {

        public static void Draw(this Chaser chaser, SpriteBatch _spriteBatch)
        {

            _spriteBatch.DrawCircle(new Vector2((float)chaser.X, (float)chaser.Y), 20, 16, new Color(255, 255, 255, 50), 2);
            _spriteBatch.DrawLine(new Vector2((float)chaser.AimX, (float)chaser.AimY), new Vector2((float)chaser.X, (float)chaser.Y), Color.White * 0.4f, 1);
           // _spriteBatch.DrawCircle(new Vector2((float)chaser.AimX, (float)chaser.AimY),3,100,Color.White, 2);


        }
        public static void Draw(this Chaser chaser, SpriteBatch _spriteBatch, Texture2D texture)
        {

            var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            _spriteBatch.Draw(texture, new Rectangle((int)chaser.X, (int)chaser.Y, (int)chaser.Width, (int)chaser.Height), null, Color.White,(float)-chaser.AimAngle, origin, SpriteEffects.FlipVertically, 0f);
            _spriteBatch.DrawLine(new Vector2((float)chaser.AimX, (float)chaser.AimY), new Vector2((float)chaser.X, (float)chaser.Y), Color.White * 0.4f, 1);
            _spriteBatch.DrawCircle(new Vector2((float)chaser.AimX, (float)chaser.AimY), 3, 100, Color.White, 2);


        }
    }
}
