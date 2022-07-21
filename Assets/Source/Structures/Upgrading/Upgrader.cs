using System;
using System.Collections.Generic;
using System.Linq;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using Lomztein.BFA2.UI.ToolTip;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Structures.Upgrading
{
    public abstract class Upgrader : MonoBehaviour, IContextMenuOptionProvider, IPurchasable
    {
        [ModelProperty]
        public string UpgradeName;
        [ModelProperty]
        public string UpgradeDescription;
        [ModelProperty]
        public ContentSpriteReference UpgradeSprite;
        [ModelProperty, SerializeReference, SR]
        public IResourceCost UpgradeCost;

        public string Name => UpgradeName;
        public string Description => UpgradeDescription;
        public virtual IResourceCost Cost => UpgradeCost;
        public virtual Sprite Sprite => UpgradeSprite.Get();

        protected abstract bool Upgrade();
        protected abstract bool CanUpgrade();
        protected abstract bool ShowUpgrade();
        protected abstract string GetStatus();

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            if (ShowUpgrade())
            {
                string status = GetStatus(); // Bit hacky but it works for now. TODO: Implement proper status thing.
                status = string.IsNullOrEmpty(status) ? string.Empty : "\n" + status; 

                return new IContextMenuOption[]
                {
                    new ContextMenuOption (() => Sprite, () => Color.white, () => UI.ContextMenu.ContextMenu.Side.Right, Upgrade, CanUpgrade, () => SimpleToolTip.InstantiateToolTip("Upgrade - " + Cost.Format(false) + status, Description)),
                };
            }
            else
            {
                return Array.Empty<IContextMenuOption>();
            }
        }
    }
}
