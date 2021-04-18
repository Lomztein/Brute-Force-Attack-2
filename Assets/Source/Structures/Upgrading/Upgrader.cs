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
        public string UpgradeDecsription;
        [ModelProperty]
        public ContentSpriteReference UpgradeSprite;
        [ModelProperty, SerializeReference, SR]
        public IResourceCost UpgradeCost;

        public string Name => UpgradeName;
        public string Description => UpgradeDecsription;
        public virtual IResourceCost Cost => UpgradeCost;
        public Sprite Sprite => UpgradeSprite.Get();

        public abstract bool Upgrade();

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            return new IContextMenuOption[]
            {
                new ContextMenuOption ("Upgrade - " + Cost.Format(), Description, Sprite, Upgrade, () => Player.Player.Resources.HasEnough(Cost))
            };
        }
    }
}
