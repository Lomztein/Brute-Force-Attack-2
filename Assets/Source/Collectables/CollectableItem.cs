using Lomztein.BFA2.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Collectables
{
    public class CollectableItem : CollectableBase
    {
        public Item Item { get; set; }

        public override void Collect()
        {
            GetComponent<IInventory>().AddItem(Item);
        }
    }
}
