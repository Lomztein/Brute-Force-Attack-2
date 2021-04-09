using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public class SingleResourceCost : IResourceCost
    {
        public Resource Resource;
        public int Amount;

        public SingleResourceCost () { }

        public SingleResourceCost (Resource resource, int amount)
        {
            Resource = resource;
            Amount = amount;
        }

        public Dictionary<Resource, int> GetCost()
        {
            return new Dictionary<Resource, int>() { { Resource, Amount } };
        }
    }
}
