using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Collectables
{
    public class CollectableItem : CollectableBase
    {
        [ModelProperty]
        public ContentPrefabReference Reference;

        public override void Collect()
        {
            GetComponent<IInventory>().AddItem(Reference.Instantiate().GetComponent<Item>());
        }
    }
}
