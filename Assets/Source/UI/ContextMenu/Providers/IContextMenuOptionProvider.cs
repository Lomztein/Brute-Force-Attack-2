using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu.Providers
{
    public interface IContextMenuOptionProvider
    {
        IEnumerable<IContextMenuOption> GetContextMenuOptions();
    }
}