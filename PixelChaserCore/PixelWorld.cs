using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Threading;

namespace PixelChaser
{
    public class PixelWorld
    {
        public bool CollisionsEnabled { get; set; } = true;
        public string Name { get; set; } = "default";
        public List<PixelUnit> PixelUnits { get; private set; } = new List<PixelUnit>();
        public List<Projectile> Projectiles { get; private set; } = new List<Projectile>();
        public List<Enemy> Enemies { get; private set; } = new List<Enemy>();
        public List<Entity> Entities { get; private set; } = new List<Entity>();
        public Chaser Chaser { get; private set; }

        public Dictionary<string, PWObject> Insiders { get; private set; } = new Dictionary<string, PWObject>();

        public CollisionChecker CollisionChecker { get; private set; }

        public List<string> IDList
        {
            get
            {
                List<string> ids = new List<string>();

                for (int i = 0; i < Entities.Count; i++)
                {
                    ids.Add(Entities[i].ID);
                }

                return ids;
            }
        }

        public event EventHandler MovedDown;
        public PixelGenerator Generator { get; private set; }
        private RandomGenerator _rdm = new RandomGenerator();
        public int MoveCounter { get; private set; } = 0;
        public int MaxEntityCount { get; private set; } = 20;

        public int Width { get; private set; } = 50;
        public int Height { get; private set; } = 50;
        public bool Locked { get; set; }

        public Point Cursor { get; private set; }

        public int MaxUnits { get; set; }

        public int MaxPixelSize { get; set; } = 16;
        public double MaxPixelSpeed { get; set; } = 10;
        public int MaxUnitsPerRow { get; set; } = 20;

        private float _areaDensityFactor = 1.2f;
        private float _densityHysteresis = 0;
        public float AreaDensityFactor
        {
            get { return _rdm.Next((int)(_areaDensityFactor - DensityHysteresis), (int)(_areaDensityFactor + DensityHysteresis)); }
            set
            {
                if (value < 0)
                    _areaDensityFactor = 0;
                else
                    _areaDensityFactor = value;
            }
        }
        public float DensityHysteresis
        {
            get { return _densityHysteresis; }
            set
            {
                if (value < 0)
                    _densityHysteresis = 0;
                else
                    _densityHysteresis = value;
            }
        }

        public List<int> AvailablePixelUnitTypes { get; private set; } = new List<int>() { 0, 1, 2, 3 };

        public PixelWorld(int width, int height)
        {
            Width = width;
            Height = height;
            Generator = new PixelGenerator(this);
            CollisionChecker = new CollisionChecker(this);

        }

        public void AddPWObject(PWObject pwObject )
        {
            if (Insiders.Keys.Contains(pwObject.ID))
                return;

            Insiders.Add(pwObject.ID, pwObject);
        }

        public void ClearPixels()
        {
            PixelUnits.Clear();
        }

        public void SetChaserEntity(Chaser chaser)
        {
            Chaser = chaser;
        }

        public void MoveDown()
        {
            if (MovedDown != null)
                MovedDown(this, new EventArgs());

            MoveCounter++;
            CleanupObjects();

        }


        public void CleanupObjects()
        {
            if (PixelUnits.Count > 0)
                try
                {
                    PixelUnits.RemoveAll(pu => pu.Y >= Height || pu.Y < 0 || pu.X >= Width || pu.X < 0);
                    Projectiles.RemoveAll(p => p.IsDead);
                    Entities.RemoveAll(ent => ent.IsDead && ent.TypeName.ToLower() != "chaser");
                }
                catch { }
        }

       

        public void CheckForProjectileCollisons()
        {
            for(int e_ix = 0; e_ix < Entities.Count; e_ix++)
            {
                Entity ent = Entities[e_ix];

                List<Projectile> closeBullets = Projectiles.Where(pr => pr.GetDistanceFrom(new PointF(ent.X, ent.Y)) <= Math.Max(ent.Height, ent.Width)).ToList();
                for (int p_ix = 0; p_ix < closeBullets.Count; p_ix++)
                    ent.CollisionData.CheckProjectileCollision(closeBullets[p_ix]);
}
        }

    }
}
