using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Shapes;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PixelChaser
{
    public static class EntityDrawingExtensions
    {

        public static Dictionary<string, Color> DefaultColors
        {
            get
            {
                Dictionary<string, Color> colors = new Dictionary<string, Color>();
                colors.Add("",Color.Gray);
                return colors;
            }
        }

        public static void Draw(this Chaser chaser, SpriteBatch _spriteBatch)
        {

            _spriteBatch.DrawCircle(new Vector2(chaser.X, chaser.Y), 20, 16, new Color(255, 255, 255, 50), 2);
            _spriteBatch.DrawLine(new Vector2(chaser.AimX, chaser.AimY), new Vector2(chaser.X, chaser.Y), Color.White * 0.4f, 1);
           // _spriteBatch.DrawCircle(new Vector2((float)chaser.AimX, (float)chaser.AimY),3,100,Color.White, 2);


        }
        public static void Draw(this Chaser chaser, SpriteBatch _spriteBatch, Texture2D texture)
        {

            var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            _spriteBatch.Draw(texture, new Rectangle((int)chaser.X, (int)chaser.Y, (int)chaser.Width, (int)chaser.Height), null, Color.White,(float)-chaser.AimAngle, origin, SpriteEffects.FlipVertically, 0f);
            _spriteBatch.DrawLine(new Vector2(chaser.AimX, chaser.AimY), new Vector2(chaser.X, chaser.Y), Color.White * 0.4f, 1);
            _spriteBatch.DrawCircle(new Vector2(chaser.AimX, chaser.AimY), 3, 100, Color.White, 2);

        }

        public static void Draw(this Entity entity, SpriteBatch _spriteBatch, Texture2D texture)
        {
                var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
                _spriteBatch.Draw(texture, new Rectangle((int)entity.X, (int)entity.Y, (int)entity.Width, (int)entity.Height), null, Color.White, (float)-entity.AimAngle, origin, SpriteEffects.FlipVertically, 0f);

        }

    }
}
