using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Targeting
{
    public interface ITarget
    {
        public Vector3 GetPosition();

        public bool IsValid();
    }
}
