﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Purchasing.Resources
{
    [Serializable]
    public class ResourceCost : IResourceCost
    {
        public Element[] Elements;

        [Serializable]
        public class Element
        {
            public Resource Type;
            public int Value;
        }

        public Dictionary<Resource, int> GetCost()
        {
            return Elements.ToDictionary(x => x.Type, y => y.Value);
        }
    }
}
