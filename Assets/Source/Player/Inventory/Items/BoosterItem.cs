using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Inventory.Items
{
    public class BoosterItem : Item
    {
        [ModelAssetReference]
        public Mod Mod;
        [ModelProperty]
        public float Coeffecient;
    }
}
