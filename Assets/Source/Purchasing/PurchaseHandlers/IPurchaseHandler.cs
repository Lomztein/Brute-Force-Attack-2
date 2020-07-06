using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Purchasing.PurchaseHandlers
{
    public interface IPurchaseHandler
    {
        void Handle(IPurchasable purchasable, IResourceContainer resources);
    }
}
