using Lomztein.BFA2.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies
{
    public interface IEnemy : IIdentifiable
    {
        float DifficultyValue { get; }

        void Init(EnemySpawnPoint point);

        event Action<IEnemy> OnKilled;
        event Action<IEnemy> OnFinished;
    }
}
