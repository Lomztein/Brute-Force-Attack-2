using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Purchasing.UI
{
    public interface IPurchaseButton
    {
        void Assign(IPurchasable purchasable, Action onSelectedCallback);
    }
}
