using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Loot
{
    public class LootInfo : MonoBehaviour
    {
        [ModelProperty]
        public Vector2Int Amount;
        [ModelProperty]
        public float Chance;
    }
}
