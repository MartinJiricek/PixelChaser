using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace PixelChaser
{
    public class SelfUpgraderGun:Gun
    {
        public override string Name { get { return "Self-Upgrade Gun"; } }

        public SelfUpgraderGun(Entity entity)
        {
            LoadEntity(entity);
            this.Cooldown = 40;
            this.Damage = 1;
            this.ProjectileLength = 3;
            this.ProjectileSpeed = 5;
            this.ProjectileWidth = 1;
        }

        public override void OnHit(Projectile projectile)
        {
            Upgrade(projectile.Hits);
            base.OnHit(projectile);
        }

        private void Upgrade(int hitCount)
        {

            if (hitCount < 1)
                return;
            
            if(Cooldown > 5)
                Cooldown--;

            Damage++;
            if(ProjectileLength < 50)
                ProjectileLength++;

            if (ProjectileSpeed < 500)
                ProjectileSpeed++;

            //Entity.CurrentWorld.Generator.MaxBatchSize++;
            //Entity.CurrentWorld.Generator.MaxSpeed++;
            //Entity.CurrentWorld.Generator.MaxUnitsOnBottom--;
            //Entity.CurrentWorld.Generator.MaxUnitsOnLeft--;
            //Entity.CurrentWorld.Generator.MaxUnitsOnTop++;
            //Entity.CurrentWorld.Generator.MaxUnitsOnRight++;


          //  Entity.CurrentWorld.Generator.Direction = PixelGenerator.VelocityDirection.RightToLeft;
        }
    }
}
