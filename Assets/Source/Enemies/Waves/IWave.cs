using Lomztein.BFA2.Player.Health;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Waves
{
    public interface IWave
    {
        void Start(IResourceContainer rewardTarget, IHealthContainer damageTarget);

        event Action<IEnemy> OnEnemySpawn;
        event Action<IEnemy> OnEnemyKill;
        event Action<IEnemy> OnEnemyFinish;
        event Action OnFinished;
    }
}
