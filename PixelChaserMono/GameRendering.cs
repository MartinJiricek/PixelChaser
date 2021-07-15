using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Shapes;
using System;

namespace PixelChaser
{
    public partial class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private System.Windows.Forms.Timer _worldTimer;
        private System.Windows.Forms.Timer _fpsCounter;
        private int _fps = 0;
        public int FPS { get; private set; }
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
        private SpriteFont _infoFont;
        private Texture2D _cursor;

        public Game1()
        {
            IsFixedTimeStep = false;

            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 580;
            //_graphics.ToggleFullScreen();

            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;

            _graphics.ApplyChanges();

            _fpsCounter = new System.Windows.Forms.Timer();
            _fpsCounter.Interval = 1000;
            _fpsCounter.Tick += FPS_Tick;
            _fpsCounter.Start();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

        private void InputProcessing()
        {

            if (KeysNow.IsKeyDown(Keys.Escape))
                Exit();

            if (KeysNow.IsKeyDown(Keys.W))            
                Chaser.MoveUp();            

            if (KeysNow.IsKeyDown(Keys.A) )
                Chaser.MoveLeft();

            if (KeysNow.IsKeyDown(Keys.S))
                Chaser.MoveDown();

            if (KeysNow.IsKeyDown(Keys.D) )
                Chaser.MoveRight();

            if (KeysNow.IsKeyDown(Keys.Space))
            {
                World.MoveDown();
            }

            if (MouseNow.LeftButton == ButtonState.Pressed)
            {
                OnMouseClick();
            }

            if (KeysNow.IsKeyDown(Keys.F2))
            {
                DebugWindow win = new DebugWindow(this);
                win.Show();
            }

            _keysBefore = Keyboard.GetState();
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

            DrawAllPixelUnits();
            DrawChaser();
            DrawProjectiles();

            _spriteBatch.DrawString(_infoFont, $"{World.PixelUnits.Count} pixels, {FPS} fps,   hits: {Chaser.Container.TotalPixels}", new Vector2(20, 20), Color.White);           
            _spriteBatch.End();
            _fps++;
            base.Draw(gameTime);
        }

        private void DrawChaser()
        {
            if (Math.Abs(MouseNow.X - Chaser.X) == 0)
                return;

            _spriteBatch.DrawCircle(new Vector2((float)Chaser.X, (float)Chaser.Y), 20 , 16, new Color(255, 255, 255, 50), 2);
            _spriteBatch.DrawLine(new Vector2((float)Chaser.AimX, (float)Chaser.AimY), new Vector2((float)Chaser.X, (float)Chaser.Y), Color.White*0.4f, 1);
            _spriteBatch.DrawCircle(new Vector2((float)Chaser.AimX, (float)Chaser.AimY),3,100,Color.White, 2);
        }

        private void DrawPixelUnit(PixelUnit pu)
        {
            _spriteBatch.Draw(_units[pu.TypeID], new Rectangle((int)pu.X, (int)pu.Y, pu.Width, pu.Height), new Color(255, 255, 255, 255-pu.A));
            // _spriteBatch.FillRectangle(new Rectangle(pu.X, pu.Y, pu.Width, pu.Height), new Color(pu.R, pu.G, pu.B,pu.A));
        }

        private void DrawProjectiles()
        {

            for(int i = 0; i < World.Projectiles.Count; i++)
            {
                Projectile p = World.Projectiles[i];
                _spriteBatch.DrawLine(new Vector2((float)p.X, (float)p.Y), new Vector2((float)(p.X+p.Velocity.X), (float)(p.Y + p.Velocity.Y)), Color.White, 2);
                _spriteBatch.DrawCircle(new Vector2((float)p.X, (float)p.Y), 2, 10, Color.White);
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
            Chaser.Shoot();
        }
       
    }
}
