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
    public class ExponentialResourceCost : IResourceCost
    {
        [ModelProperty]
        public ResourceCost Cost;
        [ModelProperty]
        public float Coeffecient;
        public int X { get; set; }

        public Dictionary<Resource, int> GetCost()
        {
            Dictionary<Resource, int> cost = new Dictionary<Resource, int>();

            foreach (var c in Cost.GetCost())
            {
                cost.Add(c.Key, Mathf.RoundToInt(c.Value * Mathf.Pow(Coeffecient, X)));
            }
            return cost;
        }
    }
}
