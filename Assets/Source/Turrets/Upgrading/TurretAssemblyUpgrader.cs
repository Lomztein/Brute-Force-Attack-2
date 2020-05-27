using System.Linq;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.Tooltip;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Upgrading
{
    public class TurretAssemblyUpgrader : MonoBehaviour, IUpgrader, ITooltip
    {
        public IResourceCost Cost => GetUpgradersInComponents().Select(x => x.Cost).Sum();

        public string Description => string.Join("\n", GetUpgradersInComponents().Select(x => x.Description)) + "\n\nUpgrade cost:\n\t" + Cost.Format();

        public string Text => Description;
        private bool _canUpgrade = false;

        private void Start()
        {
            Invoke("CanUpgrade", 1f);
        }

        private void CanUpgrade ()
        {
            _canUpgrade = true;
        }

        public void Upgrade()
        {
            foreach (var upgrader in GetUpgradersInComponents())
            {
                upgrader.Upgrade();
            }
        }

        private void TryUpgrade ()
        {
            if (GetResourceContainer().TrySpend(Cost))
            {
                Upgrade();
            }
        }

        private void Update()
        {
            // TODO: Mega ugly hardcoded, replace with some AssemblyUpgradeController singleton later.
            if (_canUpgrade && Input.GetMouseButtonDown(0) && GetComponent<CircleCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) {
                TryUpgrade();
            }
        }

        private IUpgrader[] GetUpgradersInComponents ()
        {
            return GetComponentsInChildren<IUpgrader>().Where(x => this != (object)x).ToArray();
        }

        private IResourceContainer GetResourceContainer() => GetComponent<IResourceContainer>();
    }
}
