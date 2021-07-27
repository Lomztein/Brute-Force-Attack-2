using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Collectables
{
    public class CollectableResource : Collectable
    {
        [ModelAssetReference]
        public Resource Resource;
        [ModelProperty]
        public int Amount;

        protected override void Collect()
        {
            Player.Player.Resources.ChangeResource(Resource, Amount);
        }
    }
}
