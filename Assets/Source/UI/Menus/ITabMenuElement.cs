using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Menus
{
    public interface ITabMenuElement
    {
        bool IsMenuEmpty { get; }
        string Name { get; }

        void OpenMenu();
        void CloseMenu();
    }
}
