using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Motors
{
    public interface IEnemyMotor
    {
        float Speed { get; set; }

        void SetPath(Vector3[] path);

        void Tick(float deltaTime);

        bool HasReachedEnd();
    }
}
