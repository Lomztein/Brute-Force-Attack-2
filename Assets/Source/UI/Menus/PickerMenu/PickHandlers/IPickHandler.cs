using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.PickerMenu.PickHandlers
{
    public interface IPickHandler
    {
        void Handle(IContentCachedPrefab pick);
    }
}
