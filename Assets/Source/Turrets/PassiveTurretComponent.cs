using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Turrets
{
    public class PassiveTurretComponent : TurretComponent
    {
        public override TurretComponentCategory Category => TurretComponentCategories.Utility;

        public override void End()
        {
        }

        public override void Init()
        {
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}
