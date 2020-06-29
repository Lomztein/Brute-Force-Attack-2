using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public class TurretSeller : MonoBehaviour, IContextMenuOptionProvider
    {
        public Sprite SellSprite;

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            return new IContextMenuOption[]
            {
                new ContextMenuOption("Sell Assembly", "Do it", SellSprite, () => Sell(), () => true)
            };
        }

        private bool Sell ()
        {
            Destroy(gameObject);
            return true;
        }
    }
}