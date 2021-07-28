using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Menus
{
    public interface ITabMenuElement
    {
        string Name { get; }

        void OpenMenu();
        void CloseMenu();
        void Init();

        event Action<ITabMenuElement> OnNameChanged;
    }
}
