using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public static class ResourceCostExtensions
    {
        public static IResourceCost Sum (this IEnumerable<IResourceCost> resources)
        {
            List<ResourceCost.Element> elements = new List<ResourceCost.Element>();

            foreach (IResourceCost cost in resources)
            {
                AddElements(cost, elements);
            }

            return new ResourceCost() { Elements = elements.ToArray() };
        }

        public static void AddElements(IResourceCost cost, List<ResourceCost.Element> elements)
        {
            foreach (var element in cost.GetCost())
            {
                ResourceCost.Element same = elements.FirstOrDefault(x => x.Resource == element.Key);
                if (same == null)
                {
                    same = new ResourceCost.Element() { Resource = element.Key };
                    elements.Add(same);
                }
                same.Value += element.Value;
            }
        }

        public static IResourceCost Subtract(this IResourceCost rhs, IResourceCost lhs)
        {
            List<ResourceCost.Element> elements = new List<ResourceCost.Element>();
            foreach (var element in rhs.GetCost())
            {
                if (lhs.GetCost().TryGetValue(element.Key, out int value))
                {
                    elements.Add(new ResourceCost.Element(element.Key, element.Value - value));
                }
                else
                {
                    elements.Add(new ResourceCost.Element(element.Key, element.Value));
                }
            }
            return new ResourceCost(elements.ToArray());
        }

        public static IResourceCost Scale(this IResourceCost cost, float scalar)
        {
            List<ResourceCost.Element> elements = new List<ResourceCost.Element>();
            foreach (var element in cost.GetCost())
            {
                elements.Add(new ResourceCost.Element(element.Key, Mathf.RoundToInt(element.Value * scalar)));
            }
            return new ResourceCost(elements.ToArray());
        }

        public static ResourceCost ToResourceCost(this IResourceCost cost)
        {
            if (cost is ResourceCost rc)
                return rc;

            List<ResourceCost.Element> elements = new List<ResourceCost.Element>();
            foreach (var element in cost.GetCost())
            {
                elements.Add(new ResourceCost.Element(element.Key, element.Value));
            }
            return new ResourceCost(elements.ToArray());
        }

        public static string Format(this IResourceCost cost)
        {
            return string.Join(", ", cost.GetCost().Select(x => x.Key.Shorthand + ": " + x.Value));
        }
    }
}
