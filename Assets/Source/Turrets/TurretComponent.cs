using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public class TurretComponent : MonoBehaviour, ITurretComponent
    {
        public ITurretAssembly Assembly { get; set; }

        [ModelProperty]
        public float PassiveHeatProduction;
    }
}