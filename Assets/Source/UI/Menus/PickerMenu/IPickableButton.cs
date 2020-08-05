using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Menus.PickerMenu
{
    public interface IPickableButton<T>
    {
        void Assign(T pickable, Action onPickedCallback);
    }
}
