using System.Collections.Generic;

namespace Lomztein.BFA2.Purchasing
{
    public interface IPurchasableCollection : IEnumerable<IPurchasable>
    {
        void AddPurchasable(IPurchasable purchasable);
        IPurchasable[] GetPurchasables();
        void RemovePurchasable(IPurchasable purchasable);
    }
}