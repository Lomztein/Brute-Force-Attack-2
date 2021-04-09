using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    [Serializable]
    public class ResourceCost : IResourceCost
    {
        [ModelProperty]
        public Element[] Elements;

        [Serializable]
        public class Element
        {
            [ModelProperty]
            public Resource Type;
            [ModelProperty]
            public int Value;
        }

        public Dictionary<Resource, int> GetCost()
        {
            return Elements.ToDictionary(x => x.Type, y => y.Value);
        }

        public override string ToString()
        {
            return this.Format();
        }
    }
}
