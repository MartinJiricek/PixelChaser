using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Shapes;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace PixelChaser
{
    public partial class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private System.Windows.Forms.Timer _worldTimer;
        private System.Windows.Forms.Timer _fpsCounter;
        private System.Windows.Forms.Timer _keyDelayTimer;
        private int _fps = 0;
        private SpriteFont _infoFont;
        private RandomGenerator _rdm = new RandomGenerator();
        private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        public int FPS { get; private set; }

        public int KeyDelay
        {
            get { return _keyDelayTimer.Interval; }
            set
            {
                if (value < 1)
                    _keyDelayTimer.Interval = 1;
                else if (value > 5000)
                    _keyDelayTimer.Interval = 5000;
                else
                    _keyDelayTimer.Interval = value;
            }
        }
        public KeyboardState KeysNow
        {
            get
            {
                return Keyboard.GetState();
            }
        }
        private KeyboardState _keysBefore;
        public MouseState MouseNow
        {
            get
            {
                return Mouse.GetState();
            }
        }
        public bool ShowDebugInfo { get; set; } = true;
        public bool HighlightCornerPoints { get; set; } = false;

        public bool UseFullResolution { get; set; } = false;
        public int PreSetWidth { get { return 800; } } 
        public int PreSetHeight { get { return 640; } }

        public bool ExitOnChaserDied { get; set; } = true;

        public Game1()
        {
            IsFixedTimeStep = false;

            _graphics = new GraphicsDeviceManager(this);
            Window.IsBorderless = true;
            Window.Position = new Point(0, 0);
            _graphics.SynchronizeWithVerticalRetrace = false;

            if (UseFullResolution)
                UseFullScreenResolution();
            else
                UsePreSetResolution();

            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;

            _graphics.ApplyChanges();

            _fpsCounter = new System.Windows.Forms.Timer();
            _fpsCounter.Interval = 1000;
            _fpsCounter.Tick += FPS_Tick;
            _fpsCounter.Start();


            _keyDelayTimer = new System.Windows.Forms.Timer();
            _keyDelayTimer.Interval = 30;
            _keyDelayTimer.Tick += KeyDelay_Tick;


            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        private void LoadTextures()
        {
            LoadTexture("CursorBasic");
            LoadTexture("LaserGreen");
            LoadTexture("Chaser2");
            LoadTexture("Enemy1");
        }

        private void LoadTexture(string textureName)
        {
            if (_textures.ContainsKey(textureName))
                return;
            try
            {
                _textures.Add(textureName, Content.Load<Texture2D>(textureName));
            }
            catch { }
        }

        public void UseFullScreenResolution()
        {
            _graphics.PreferredBackBufferWidth = PreSetWidth;
            _graphics.PreferredBackBufferHeight = PreSetHeight;
        }

        public void UsePreSetResolution()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }

        private void KeyDelay_Tick(object sender, EventArgs e)
        {
            _keyDelayTimer.Enabled = false;
        }

        private void FPS_Tick(object sender, EventArgs e)
        {
            FPS = _fps;
            _fps = 0;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _infoFont = Content.Load<SpriteFont>("DefaultFont");

            LoadWorld(new PixelWorld(Window.ClientBounds.Width, Window.ClientBounds.Height));

            Mouse.SetCursor(MouseCursor.FromTexture2D(_textures["CursorBasic"], _textures["CursorBasic"].Width / 2, _textures["CursorBasic"].Height / 2));
            IsMouseVisible = false;
        }

        protected override void Update(GameTime gameTime)
        {
            InputProcessing();

            Chaser.AimX = MouseNow.X;
            Chaser.AimY = MouseNow.Y;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

            //  Chaser.Draw(_spriteBatch,_chaser);
            DrawProjectiles();
            DrawAllPixelUnits();
            DrawEntities();

            if (ShowDebugInfo)
                DrawGameInfo();

            _spriteBatch.End();
            _fps++;
            base.Draw(gameTime);
        }

        private void DrawEntities()
        {
            for (int i = 0; i < World.Entities.Count; i++)
            {
                Entity ent = World.Entities[i];
                if (!ent.IsDead)
                    switch (ent.TypeName)
                    {
                        case "enemy":
                            ent.Draw(_spriteBatch, _textures[ent.TextureID]);
                            if (DrawAllAimingLines)
                                _spriteBatch.DrawLine(new Vector2(ent.X, ent.Y), new Vector2(ent.AimX, ent.AimY), Color.Gray, 0.5f);
                            break;

                        case "chaser":
                            ent.Draw(_spriteBatch, _textures[ent.TextureID]);
                            if (DrawChaserAimingLine || DrawAllAimingLines)
                            {
                                _spriteBatch.DrawLine(new Vector2(ent.X, ent.Y), new Vector2(ent.AimX, ent.AimY), Color.Gray, 0.5f);
                                for(int ix = 0; ix < ent.CollisionData.Count;ix++)
                                {
                                    Vector2 end = new Vector2(ent.CollisionData[0].X, ent.CollisionData[0].Y);
                                    if(ix < ent.CollisionData.Count-1)
                                        end = new Vector2(ent.CollisionData[ix+1].X, ent.CollisionData[ix+1].Y);
                                    _spriteBatch.DrawLine(new Vector2(ent.CollisionData[ix].X, ent.CollisionData[ix].Y), end, Color.White);

                                }
                            }

                            break;
                    }

                if (HighlightCornerPoints)
                {

                    _spriteBatch.DrawCircle(new Vector2(ent.X, ent.Y), 2, 5, Color.Blue, 5);
                    _spriteBatch.DrawCircle(new Vector2(ent.X - ent.Width / 2, ent.Y - ent.Height / 2), 2, 4, Color.Blue, 5);
                    _spriteBatch.DrawCircle(new Vector2(ent.X + ent.Width / 2, ent.Y - ent.Height / 2), 2, 4, Color.Blue, 5);
                    _spriteBatch.DrawCircle(new Vector2(ent.X - ent.Width / 2, ent.Y + ent.Height / 2), 2, 4, Color.Blue, 5);
                    _spriteBatch.DrawCircle(new Vector2(ent.X + ent.Width / 2, ent.Y + ent.Height / 2), 2, 4, Color.Blue, 5);

                    for (int ix = 0; ix < ent.CollisionData.Count; ix++)
                        _spriteBatch.DrawCircle(new Vector2(ent.CollisionData[ix].X, ent.CollisionData[ix].Y), 2, 5, Color.White, 5);


                    for (int ix = 0; ix < World.Projectiles.Count; ix++)
                    {
                        Projectile p = World.Projectiles[ix];
                        _spriteBatch.DrawCircle(new Vector2(p.X, p.Y), 2, 5, Color.White, 5);
                        _spriteBatch.DrawCircle(new Vector2(p.X + p.Velocity.X, p.Y + p.Velocity.Y), 2, 5, Color.White, 5);

                    }

                    try
                    {
                        _spriteBatch.DrawCircle(new Vector2(ent.Gun.X, ent.Gun.Y), 2, 4, Color.LimeGreen, 5);
                    }
                    catch { }

                }

            }
        }

        public bool DrawAllAimingLines { get; set; } = true;
        public bool DrawChaserAimingLine { get; set; } = true;


        private void DrawPixelUnit(PixelUnit pu)
        {
            _spriteBatch.Draw(_units[pu.TypeID], new Rectangle((int)(pu.X - pu.Width / 2), (int)(pu.Y - pu.Height / 2), (int)pu.Width, (int)pu.Height), new Color(255, 255, 255, _rdm.Next(0, 255)));
        }

        private void DrawProjectiles()
        {
            var origin = new Vector2(_laserGreen.Width / 2f, _laserGreen.Height / 2f);

            for (int i = 0; i < World.Projectiles.Count; i++)
            {
                Projectile p = World.Projectiles[i];
                //_spriteBatch.Draw(_laserGreen, new Rectangle((int)p.X, (int)p.Y, (int)p.Velocity.Y, (int)p.Velocity.X), null, Color.White, -p.Angle, origin, SpriteEffects.None, 0f);
                _spriteBatch.DrawLine(new Vector2(p.X, p.Y), new Vector2(p.Velocity.X + p.X, p.Velocity.Y+ p.Y), Color.LimeGreen, 2);
            }
        }

        private void DrawAllPixelUnits()
        {
            for (int i = 0; i < World.PixelUnits.Count; i++)
            {
                DrawPixelUnit(World.PixelUnits[i]);
            }
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
            _spriteBatch.Dispose();
        }

        private void OnMouseClick()
        {
            Chaser.Gun.Shoot();
        }

        private void DrawGameInfo()
        {
            // arial 10, 15px per line

            StringBuilder sb = new StringBuilder();


            sb.AppendLine($"Pixels: {World.PixelUnits.Count}    Projectiles: {World.Projectiles.Count}");
            sb.AppendLine($"Entities: {World.Entities.Count}    Projectiles: {World.Projectiles.Count}");
            sb.AppendLine($"FPS: {FPS}");
            sb.AppendLine($"HP: {Chaser.HP}    Hits: {Chaser.Hits}");
            sb.AppendLine($"Lifetime: {Chaser.LifeTime} ticks");
            sb.AppendLine($"CD: {Chaser.Gun.Cooldown}");
            sb.AppendLine($"Damage: {Chaser.Gun.Damage}");
            sb.AppendLine($"Projectile Length: {Chaser.Gun.ProjectileLength}");
            sb.AppendLine($"Projectile Speed: {Chaser.Gun.ProjectileSpeed}");
            sb.AppendLine($"Current gun: {Chaser.Gun.Name}");
            sb.AppendLine($"Position: [{Chaser.X}, {Chaser.Y}]   Velocity: [{Chaser.Velocity.X}, {Chaser.Velocity.Y}]");
            sb.AppendLine($"Aim: [{Chaser.AimX}, {Chaser.AimY}]  Angle: [{Chaser.Angle * (180 / Math.PI)}]");
            sb.AppendLine($"Gun Position: [{Chaser.Gun.InitialX}, {Chaser.Gun.InitialY}]");

            _spriteBatch.DrawString(_infoFont, sb.ToString(), new Vector2(5, 10), Color.White);
            sb.Clear();




            for (int i = 0; i < World.Entities.Count; i++)
            {
                Entity ent = World.Entities[i];
                float x = ent.X - ent.Width / 2;
                float y = ent.Y + ent.Height / 2;

                _spriteBatch.DrawString(_infoFont, $"HP: {ent.HP}   HITS: {ent.Hits}", new Vector2(x, y), Color.Red);
            }

        }

    }
}
