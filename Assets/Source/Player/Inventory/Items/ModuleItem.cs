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
    [CreateAssetMenu(menuName = "BFA2/Items/ModuleItem", fileName = "New Module Item")]
    public class ModuleItem : Item
    {
        [ModelAssetReference]
        public Mod Mod;
        [ModelProperty]
        public float Coeffecient;
        [ModelProperty]
        public int ModuleSlots;
    }
}
