using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.Buttons
{
    public interface IPickableButton
    {
        void Assign(IContentCachedPrefab pickable, Action onPickedCallback);
    }
}
