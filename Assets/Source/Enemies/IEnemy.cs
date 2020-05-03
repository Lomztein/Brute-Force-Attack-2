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
        void SetPosition(Vector3 position);

        void SetOnDeathCallback(Action onDeath);
    }
}
