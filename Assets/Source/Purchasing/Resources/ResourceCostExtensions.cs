using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static void AddElements(IResourceCost cost, List<ResourceCost.Element> elements)
        {
            foreach (var element in cost.GetCost())
            {
                ResourceCost.Element same = elements.FirstOrDefault(x => x.Type == element.Key);
                if (same == null)
                {
                    same = new ResourceCost.Element() { Type = element.Key };
                    elements.Add(same);
                }
                same.Value += element.Value;
            }
        }
    }
}
