using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public interface IResourceContainer
    {
        event Action<Resource, int, int> OnResourceChanged;

        int GetResource(Resource resource);

        void ChangeResource(Resource resource, int value);

        void SetResource(Resource resource, int value);
    }

    public static class ResourceContainerExtensions
    {
        public static bool HasEnough(this IResourceContainer container, IResourceCost cost)
            => cost.GetCost().All(x => container.GetResource(x.Key) >= x.Value);

        public static bool TrySpend (this IResourceContainer container, IResourceCost cost)
        {
            if (HasEnough (container, cost))
            {
                foreach (var element in cost.GetCost())
                {
                    container.ChangeResource(element.Key, -element.Value);
                }
                return true;
            }
            return false;
        }

        public static void AddResources (this IResourceContainer container, IResourceCost resources)
        {
            foreach (var resource in resources.GetCost())
            {
                container.ChangeResource(resource.Key, resource.Value);
            }
        }
    }
}
