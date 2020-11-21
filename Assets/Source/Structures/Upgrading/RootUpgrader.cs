using System.Collections.Generic;
using System.Linq;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using Lomztein.BFA2.UI.Tooltip;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Upgrading
{
    public class RootUpgrader : MonoBehaviour, IUpgrader, IContextMenuOptionProvider
    {
        public Sprite UpgradeSprite;
        public IResourceCost Cost => GetUpgradersInComponents().Select(x => x.Cost).Sum();

        public string Description => string.Join("\n", GetUpgradersInComponents().Select(x => x.Description));
        
        public void Upgrade()
        {
            foreach (var upgrader in GetUpgradersInComponents())
            {
                upgrader.Upgrade();
            }
            SendMessage("OnAssemblyUpdated");
        }

        private bool TryUpgrade ()
        {
            if (GetResourceContainer().TrySpend(Cost))
            {
                Upgrade();
                return true;
            }
            return false;
        }

        private bool CanUpgrade() => GetResourceContainer().HasEnough(Cost);

        private IUpgrader[] GetUpgradersInComponents ()
        {
            return GetComponentsInChildren<IUpgrader>().Where(x => this != (object)x).ToArray();
        }

        private IResourceContainer GetResourceContainer() => GetComponent<IResourceContainer>();

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            return new IContextMenuOption[]
            {
                new ContextMenuOption ("Upgrade - " + Cost.Format(), Description, UpgradeSprite, () => TryUpgrade(), () => CanUpgrade())
            };
        }
    }
}
