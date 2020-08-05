using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Collectables
{
    public class CollectableResource : CollectableBase
    {
        [ModelProperty]
        public ResourceCost Resources;

        public override void Collect()
        {
            GetComponent<IResourceContainer>().AddResources(Resources);
        }
    }
}
