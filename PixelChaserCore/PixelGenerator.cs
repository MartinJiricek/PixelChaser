using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PixelChaser
{
    public class PixelGenerator
    {

        public PixelWorld World { get; private set; }
        public bool GenerateOnWorldTick { get; set; } = true;
        public bool SymetricSizeEnabled { get; set; } = true;

        private int _minSize = 1;
        private int _maxSize = 8;
        public int MinSize
        {
            get { return _minSize; }
            set
            {
                if (value < 1)
                    _minSize = 1;
                else
                    _minSize = value;
            }
        }
        public int MaxSize
        {
            get { return _maxSize; }
            set
            {
                if (value < _minSize)
                    _maxSize = _minSize;
                else
                    _maxSize = value;
            }
        }
        public int MaxSpeed { get; set; } = 32;
        public int MaxUnitsOnTop { get; set; } = 5;
        public int MaxUnitsOnRight { get; set; } = 5;
        public int MaxUnitsOnBottom { get; set; } = 5;
        public int MaxUnitsOnLeft { get; set; } = 5;


        private int _maxBatchSize = 5;
        private int _minBatchSize = 0;
        public int MaxBatchSize
        {
            get { return _maxBatchSize; }
            set
            {
                if (value < _minBatchSize)
                    _maxBatchSize = _minBatchSize;
                else
                    _maxBatchSize = value;
            }
        }
        public int MinBatchSize
        {
            get { return _minBatchSize; }
            set
            {
                if (value < 0)
                    _minBatchSize = 0;
                else if (value > _maxBatchSize)
                    _minBatchSize = _maxBatchSize;
                else
                    _minBatchSize = value;
            }
        }

        public bool TopSpawn { get; set; } = true;
        public bool RightSpawn { get; set; } = true;
        public bool BottomSpawn { get; set; } = true;
        public bool LeftSpawn { get; set; } = false;

        public VelocityDirection Direction { get; set; } = VelocityDirection.RightToLeft;
        public int MaxVelocityExtension { get; set; } = 5;

        public bool EnableProjectileCollisions { get; set; } = true;

        public bool Enabled { get; set; } = true;

        public int MinA { get; set; } = 0;
        public int MaxA { get; set; } = 255;
        public int MinR { get; set; } = 0;
        public int MaxR { get; set; } = 255;
        public int MinG { get; set; } = 0;
        public int MaxG { get; set; } = 0;
        public int MinB { get; set; } = 0;
        public int MaxB { get; set; } = 0;

        private RandomGenerator _rdm = new RandomGenerator();


        public PixelGenerator(PixelWorld world)
        {
            World = world;
            World.MovedDown += WorldTick;
            SetDefaultConfiguration();
        }

        private void WorldTick(object sender, EventArgs e)
        {
            GeneratePixelUnits();
            CheckProjectileCollisions();
        }

        public void CheckProjectileCollisions()
        {
            if(EnableProjectileCollisions)
                for (int i = 0; i < World.Projectiles.Count; i++)
                {
                    Projectile prj = World.Projectiles[i];
                    int hits = World.PixelUnits.RemoveAll(pu => xMath.LineIntersectsRect(prj.PTStart,prj.PTEnd,pu.GetRectangle()));
                    if (hits > 0)
                    {
                        //prj.AddHit(hits);
                    }
                }
        }

        public void SetDefaultConfiguration()
        {

            MaxSize = 8;
            MaxSpeed = 16;
            MaxBatchSize = 4;
            MinSize = 2;

            MinA = 0;
            MaxA = 255;

            MinR = 0;
            MaxR = 0;

            MinG = 0;
            MaxG = 0;

            MinB = 0;
            MaxB = 0;

            RightSpawn = true;
            TopSpawn = false;
            BottomSpawn = true;
            LeftSpawn = false;

            SymetricSizeEnabled = true;
            Direction = VelocityDirection.RightToLeftExtended;
        }
        public void GeneratePixelUnits()
        {
            if(Enabled)
            GeneratePixelUnits(_rdm.Next(MinBatchSize,MaxBatchSize));
        }

        public void GeneratePixelUnits(int batchSize)
        {
            if (Enabled)
                for (int i = 0; i < batchSize; i++)
                {
                    if (Enabled)
                        World.PixelUnits.Add(GetRandomPixelUnit());
                }

        }

        public PixelUnit GetRandomPixelUnit()
        {
            PixelUnit pu = new PixelUnit(World);

            pu.Velocity = GetRandomVelocity();
            pu.Position = GetRandomPoint();
            pu.TypeID = GetRandomPixelType();
            pu.Size = GetRandomSize();

            return pu;
        }

        public Size GetRandomSize()
        {
            if(SymetricSizeEnabled)
            {
                int a = _rdm.Next(MinSize, MaxSize);

                return new Size(a, a);
            }
            else
            {
                int w = _rdm.Next(MinSize, MaxSize);
                int h = _rdm.Next(MinSize, MaxSize);

                return new Size(w, h);
            }
        }

        public int GetRandomPixelType()
        {
            return _rdm.Next(0, World.AvailablePixelUnitTypes.Count - 1);
        }

        public Color GetRandomColor()
        {
            int a = _rdm.Next(MinA, MaxA);
            int r = _rdm.Next(MinR, MaxR);
            int g = _rdm.Next(MinG, MaxG);
            int b = _rdm.Next(MinB, MaxB);

            return Color.FromArgb(a, r, g, b);
        }

        public Point GetRandomPoint()
        {
            if (!BottomSpawn && !LeftSpawn && !TopSpawn && !RightSpawn)
                return new Point(0, 0);
            bool done = false;
            int counter = 0;

            while (!done)
            {
                if (counter > 10)
                    return new Point(1, 1);

                switch (_rdm.Next(0, 3))
                {
                    case 0:
                        if (TopSpawn)
                        {
                            int x = _rdm.Next(1, World.Width - 1);
                            int y = 1;
                            return new Point(x, y);
                        }
                        break;

                    case 1:
                        if (BottomSpawn)
                        {
                            int x = _rdm.Next(1, World.Width - 1);
                            int y = World.Height - 1;

                            return new Point(x, y);
                        }
                        break;
                    case 2:
                        if (RightSpawn)
                        {
                            int x = World.Width - 1;
                            int y = _rdm.Next(1, World.Height - 1);

                            return new Point(x, y);
                        }
                        break;
                    case 3:
                        if (LeftSpawn)
                        {
                            int x = 1;
                            int y = _rdm.Next(1, World.Height - 1);

                            return new Point(x, y);
                        }
                        break;
                }


            }
                return new Point(1, 1);

        }

        public enum VelocityDirection
        {
            Any = 0,
            RightToLeft = 1,
            RightToLeftExtended = 11,
            LeftToRight = -1,
            LeftToRightExtended = -11,
            TopToBottom = 2,
            TopToBottomExtended = 22,
            BottomToTop = -2,
            BottomToTopExtended = -22
        }
        public Velocity GetRandomVelocity()
        {
            return GetRandomVelocity(Direction);
        }
        public  Velocity GetRandomVelocity(VelocityDirection direction)
        {
            Velocity velocity = new Velocity();

            switch(direction)
            {
                case VelocityDirection.Any:
                    while (velocity.X == 0)
                        velocity.X = _rdm.Next(-MaxSpeed, MaxSpeed);
                    while (velocity.Y == 0)
                        velocity.Y = _rdm.Next(-MaxSpeed, MaxSpeed);
                    break;

                case VelocityDirection.BottomToTop:
                        velocity.X = 0;
                        velocity.Y = _rdm.Next(-MaxSpeed, -1);
                    break;

                case VelocityDirection.LeftToRight:
                    velocity.X = _rdm.Next(1, MaxSpeed);
                    velocity.Y = 0;
                    break;

                case VelocityDirection.RightToLeft:
                    velocity.X = _rdm.Next(-MaxSpeed, -1);
                    velocity.Y = 0;
                    break;

                case VelocityDirection.TopToBottom:
                    velocity.X = 0;
                    velocity.Y = _rdm.Next(1, MaxSpeed);
                    break;


                case VelocityDirection.BottomToTopExtended:
                    velocity.X = _rdm.Next(-MaxVelocityExtension, MaxVelocityExtension);
                    velocity.Y = _rdm.Next(-MaxSpeed, -1);
                    break;

                case VelocityDirection.LeftToRightExtended:
                    velocity.X = _rdm.Next(1, MaxSpeed);
                    velocity.Y = _rdm.Next(-MaxVelocityExtension, MaxVelocityExtension);
                    break;

                case VelocityDirection.RightToLeftExtended:
                    velocity.X = _rdm.Next(-MaxSpeed, -1);
                    velocity.Y = _rdm.Next(-MaxVelocityExtension, MaxVelocityExtension);
                    break;

                case VelocityDirection.TopToBottomExtended:
                    velocity.X = _rdm.Next(-MaxVelocityExtension, MaxVelocityExtension);
                    velocity.Y = _rdm.Next(1, MaxSpeed);
                    break;
            }

            return velocity;
        }
    }


}
