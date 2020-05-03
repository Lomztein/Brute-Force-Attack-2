using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Waves
{
    public interface IWave
    {
        void Start(IRoundManager manager);

        event Action<IEnemy> OnSpawn;
        event Action OnFinished;
    }
}
