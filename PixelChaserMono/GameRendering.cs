using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Shapes;
using System;
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
        private Texture2D _cursor;
        private Texture2D _laserGreen;
        private Texture2D _chaser;

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

        public Game1()
        {
            IsFixedTimeStep = false;

            _graphics = new GraphicsDeviceManager(this);
            Window.IsBorderless = true;
            Window.Position = new Point(0, 0);
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
           // _graphics.ToggleFullScreen();

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
            _cursor = Content.Load<Texture2D>("CursorBasic");
            _laserGreen = Content.Load<Texture2D>("LaserGreen");
            _chaser = Content.Load<Texture2D>("Chaser2");

            Mouse.SetCursor(MouseCursor.FromTexture2D(_cursor, _cursor.Width/2, _cursor.Height/2));
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

            // TODO: Add your drawing code here

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

            DrawAllPixelUnits();
            Chaser.Draw(_spriteBatch,_chaser);
            DrawProjectiles();


            _spriteBatch.DrawString(_infoFont, $"{World.PixelUnits.Count} pixels, {FPS} fps,   hits: {Chaser.Container.TotalPixels}", new Vector2(20, 20), Color.White);           
            _spriteBatch.End();
            _fps++;
            base.Draw(gameTime);
        }


        private void DrawPixelUnit(PixelUnit pu)
        {
            _spriteBatch.Draw(_units[pu.TypeID], new Rectangle((int)pu.X, (int)pu.Y, pu.Width, pu.Height), new Color(255, 255, 255, 255-pu.A));
            // _spriteBatch.FillRectangle(new Rectangle(pu.X, pu.Y, pu.Width, pu.Height), new Color(pu.R, pu.G, pu.B,pu.A));
        }

        private void DrawProjectiles()
        {
            RandomGenerator rdm = new RandomGenerator(); 
            var origin = new Vector2(_laserGreen.Width / 2f, _laserGreen.Height / 2f);
            Color col = new Color(255,255,255,255);

            for (int i = 0; i < World.Projectiles.Count; i++)
            {
                Projectile p = World.Projectiles[i];
                //_spriteBatch.DrawLine(new Vector2((float)p.X, (float)p.Y), new Vector2((float)(p.X+p.Velocity.X), (float)(p.Y + p.Velocity.Y)), col, 1);
                //_spriteBatch.DrawCircle(new Vector2((float)p.X, (float)p.Y), 1, 10, col,2);

                _spriteBatch.Draw(_laserGreen, new Rectangle((int)p.X, (int)p.Y, (int)Chaser.Gun.ProjectileWidth, (int)Chaser.Gun.ProjectileLength), null, col,-p.Angle, origin, SpriteEffects.None, 0f);


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

            // If you are creating your texture (instead of loading it with
            // Content.Load) then you must Dispose of it
        }

        private void OnMouseClick()
        {
            Chaser.Gun.Shoot();
        }
       
    }
}
