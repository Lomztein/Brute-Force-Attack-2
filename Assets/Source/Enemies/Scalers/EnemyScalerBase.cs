using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Scalers
{
    public abstract class EnemyScalerBase : IEnemyScaler
    {
        public abstract void Scale(Enemy enemy);
    }
}
