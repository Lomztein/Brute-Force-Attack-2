using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public interface IResourceCost
    {
        Dictionary<Resource, int> GetCost();
    }

    public class ResourceComparer : IComparer<IResourceCost>
    {
        public int Compare(IResourceCost x, IResourceCost y)
            => Math.Sign(CalculateValue(x) - CalculateValue(y));

        private float CalculateValue(IResourceCost cost)
            => cost.GetCost().Sum(x => x.Key.BinaryValue * x.Value);
    }
}
