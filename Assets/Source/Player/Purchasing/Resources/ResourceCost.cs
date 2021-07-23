using Lomztein.BFA2.ContentSystem;
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
            [ModelAssetReference]
            public Resource Resource;
            [ModelProperty]
            public int Value;

            public Element ()
            {
            }

            public Element (Resource resource, int value)
            {
                Resource = resource;
                Value = value;
            }
        }

        public ResourceCost()
        {
        }

        public ResourceCost(params Element[] elements)
        {
            Elements = elements;
        }

        public Dictionary<Resource, int> GetCost()
        {
            return Elements.ToDictionary(x => x.Resource, y => y.Value);
        }

        public override string ToString()
        {
            return this.Format();
        }
    }
}
