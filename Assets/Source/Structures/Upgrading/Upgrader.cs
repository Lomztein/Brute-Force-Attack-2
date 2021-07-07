using System.Collections.Generic;
using System.Linq;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using Lomztein.BFA2.UI.Tooltip;
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

        public abstract bool Upgrade();
        public abstract bool CanUpgrade();
        public abstract bool ShowUpgrade();
        public abstract string GetStatus();

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            if (ShowUpgrade())
            {
                string status = GetStatus(); // Bit hacky but it works for now. TODO: Implement proper status thing.
                status = string.IsNullOrEmpty(status) ? string.Empty : "\n" + status; 

                return new IContextMenuOption[]
                {
                    new ContextMenuOption ("Upgrade - " + Cost.Format() + status, Description, Sprite, Upgrade, CanUpgrade)
                };
            }
            else
            {
                return new IContextMenuOption[0];
            }
        }
    }
}
