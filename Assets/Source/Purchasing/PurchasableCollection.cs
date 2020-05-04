using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Purchasing
{
    public class PurchasableCollection : IPurchasableCollection
    {
        private List<IPurchasable> _purchasables;

        public PurchasableCollection (IEnumerable<IPurchasable> purchasable)
        {
            _purchasables = purchasable.ToList();
        }

        public void AddPurchasable(IPurchasable purchasable) => _purchasables.Add(purchasable);

        public IEnumerator<IPurchasable> GetEnumerator()
        {
            return ((IEnumerable<IPurchasable>)_purchasables).GetEnumerator();
        }

        public void RemovePurchasable(IPurchasable purchasable) => _purchasables.Remove(purchasable);

        public IPurchasable[] GetPurchasables() => _purchasables.ToArray();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IPurchasable>)_purchasables).GetEnumerator();
        }
    }
}
