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

        Vector3[] Path { get; set; }
        int PathIndex { get; set; }

        void Tick(float deltaTime);

        bool HasReachedEnd();
    }
}
