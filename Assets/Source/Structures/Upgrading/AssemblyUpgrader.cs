using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Upgrading
{
    public class AssemblyUpgrader : MonoBehaviour, IContextMenuOptionProvider
    {
        private TurretAssembly _assembly;

        private void Start()
        {
            _assembly = GetComponent<TurretAssembly>();            
        }

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            IEnumerable<Tier> next = _assembly.Tiers.Where(x => _assembly.UpgradeMap.GetNext(_assembly.CurrentTeir).Any(y => x.Equals(y)));
            foreach (Tier tier in next)
            {
                yield return GenerateOption(new Tier(tier.Name, tier.TierIndex, tier.VariantIndex)); // This is probably being straight up abuse to memory lol.
            }
        }

        private IContextMenuOption GenerateOption (Tier tier)
        {
            return new ContextMenuOption(() => "Upgrade to " + tier.Name,
                    () => GetCost(tier).Format(),
                    () => Iconography.GenerateSprite(_assembly.GetTierParent(tier).gameObject),
                    () => null,
                    () => UI.ContextMenu.ContextMenu.Side.Right,
                    () => UpgradeTo(tier),
                    () => CanUpgradeTo(tier));
        }

        private IResourceCost GetCost (Tier tier)
            => _assembly.GetCost(tier).Subtract(_assembly.GetCost(_assembly.CurrentTeir));

        private bool UpgradeTo (Tier tier)
        {
            var cost = GetCost(tier);
            if (Player.Player.Resources.TrySpend(cost)) {
                _assembly.SetTier(tier);
                _assembly.InvokeHierarchyChanged(_assembly.gameObject, this);
            }
            return true;
        }

        private bool CanUpgradeTo (Tier tier)
        {
            var cost = GetCost(tier);
            return Player.Player.Resources.HasEnough(cost) && _assembly.GetComponents(tier).All(x => Player.Player.Unlocks.IsUnlocked(x.Identifier));
        }
    }
}
