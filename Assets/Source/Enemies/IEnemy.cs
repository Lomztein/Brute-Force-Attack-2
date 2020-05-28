using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies
{
    public interface IEnemy
    {
        float DifficultyValue { get; }
        void Init(Vector3 position);

        event Action<IEnemy> OnKilled;
        event Action<IEnemy> OnFinished;
    }
}
