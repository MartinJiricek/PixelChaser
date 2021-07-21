using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Shapes;
using System;
using System.Text;
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
        private RandomGenerator _rdm = new RandomGenerator();
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
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

            Chaser.Draw(_spriteBatch,_chaser);
            DrawProjectiles();
            DrawAllPixelUnits();
            if (ShowDebugInfo)
             DrawGameInfo();
       
            _spriteBatch.End();
            _fps++;
            base.Draw(gameTime);
        }

        private void DrawEntities()
        {
            for(int i = 0; i < World.Entities.Count; i++)
            {
                Entity e = World.Entities[i];
            }
        }


        private void DrawPixelUnit(PixelUnit pu)
        {
            _spriteBatch.Draw(_units[pu.TypeID], new Rectangle((int)(pu.X-pu.Width/2), (int)(pu.Y - pu.Height / 2),(int) pu.Width, (int)pu.Height), new Color(255, 255, 255, _rdm.Next(0,255)));
        }

        private void DrawProjectiles()
        {
            var origin = new Vector2(_laserGreen.Width / 2f, _laserGreen.Height / 2f);

            for (int i = 0; i < World.Projectiles.Count; i++)
            {
                Projectile p = World.Projectiles[i];
                _spriteBatch.Draw(_laserGreen, new Rectangle((int)p.X, (int)p.Y, (int)Chaser.Gun.ProjectileWidth, (int)Chaser.Gun.ProjectileLength), null, Color.White,-p.Angle, origin, SpriteEffects.None, 0f);
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
            sb.AppendLine($"FPS: {FPS}");
            sb.AppendLine($"HP: {Chaser.HP}    Hits: {Chaser.Hits}");
            sb.AppendLine($"Lifetime: {Chaser.LifeTime} ticks");
            sb.AppendLine($"CD: {Chaser.Gun.Cooldown}");
            sb.AppendLine($"Damage: {Chaser.Gun.Damage}");
            sb.AppendLine($"Projectile Length: {Chaser.Gun.ProjectileLength}");
            sb.AppendLine($"Projectile Speed: {Chaser.Gun.ProjectileSpeed}");
            sb.AppendLine($"Current gun: {Chaser.Gun.Name}");
            sb.AppendLine($"Position: [{Chaser.X}, {Chaser.Y}]   Velocity: [{Chaser.Velocity.X}, {Chaser.Velocity.Y}]");
            sb.AppendLine($"Aim: [{Chaser.AimX}, {Chaser.AimY}]  Angle: [{Chaser.AimAngle}]");



            _spriteBatch.DrawString(_infoFont, sb.ToString(), new Vector2(5, 10), Color.White);



        }
    }
}
