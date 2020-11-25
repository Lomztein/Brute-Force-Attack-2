using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public class TurretSeller : MonoBehaviour, IContextMenuOptionProvider
    {
        [ModelProperty]
        public ContentSpriteReference SellSprite = new ContentSpriteReference();

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            return new IContextMenuOption[]
            {
                new ContextMenuOption("Sell Assembly", "Sell this assembly.", SellSprite.Get(), () => Sell(), () => true)
            };
        }

        private bool Sell ()
        {
            Destroy(gameObject);
            return true;
        }
    }
}