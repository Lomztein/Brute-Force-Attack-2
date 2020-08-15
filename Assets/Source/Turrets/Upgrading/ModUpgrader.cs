using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Upgrading
{
    public class ModUpgrader : MonoBehaviour, IUpgrader
    {
        private const string MOD_CHILD_NAME = "UpgradeMod";

        [ModelProperty]
        [SerializeField]
        private ExponentialResourceCost _cost;
        public IResourceCost Cost => _cost;
        [ModelProperty]
        public int MaxUpgrades = 10;

        private int _upgradeCount;

        public string Description => GetMod().Name;

        public void Upgrade()
        {
            if (_upgradeCount < MaxUpgrades)
            {
                GetComponent<IModdable>().Mods.AddMod(GetMod());
                _upgradeCount++;
                _cost.X = _upgradeCount;
            }
        }

        private IMod GetMod() => transform.Find(MOD_CHILD_NAME).GetComponent<IMod>();
    }
}
