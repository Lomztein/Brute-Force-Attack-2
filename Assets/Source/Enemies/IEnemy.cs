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
        void Init(Vector3 position, Vector3[] path);

        Vector3[] Path { get; set; }
        int PathIndex { get; set; }

        event Action<IEnemy> OnKilled;
        event Action<IEnemy> OnFinished;
    }
}
