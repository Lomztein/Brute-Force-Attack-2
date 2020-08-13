using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Scalers
{
    public abstract class EnemyScalerBase<T> : IEnemyScaler where T : IEnemy
    {
        public bool CanScale(IEnemy enemy) => enemy.GetType() == typeof(T);

        public void Scale(IEnemy enemy) => Scale((T)enemy);
        public abstract void Scale(T enemy);
    }
}
