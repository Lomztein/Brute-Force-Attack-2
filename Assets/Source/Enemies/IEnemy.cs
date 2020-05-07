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
        void Init(Vector3 position);

        event Action<IEnemy, int> OnDeath;
        event Action<IEnemy, float> OnFinished;
    }
}
