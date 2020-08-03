using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers.Turret;
using Lomztein.BFA2.Turrets;
using Lomztein.BFA2.UI.Tooltip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Upgrading
{
    public class TurretComponentTypeUpgrader : MonoBehaviour, IUpgrader
    {
        [ModelProperty]
        public string UpgradeComponentIdentifier;

        public string Description => $"{GetThis().Name} -> {GetUpgrade().Name}";
        public IResourceCost Cost => GetUpgrade().Cost;

        private void Upgrade()
        {
            ITurretAssembly assembly = GetComponentInParent<ITurretAssembly>();
            ITurretComponent current = GetComponent<ITurretComponent>();

            GameObject newObj = GameObjectTurretComponentAssembler.GetComponent(UpgradeComponentIdentifier).Instantiate();

            newObj.transform.position = transform.position;
            newObj.transform.rotation = transform.rotation;
            newObj.transform.localScale = transform.localScale;

            newObj.transform.SetParent(transform.parent, true);

            Destroy(gameObject);
        }

        private IPurchasable GetThis() => GetComponent<IPurchasable>();

        private IPurchasable GetUpgrade()
        {
            return GameObjectTurretComponentAssembler.GetComponent(UpgradeComponentIdentifier).GetCache().GetComponent<IPurchasable>();
        }

        void IUpgrader.Upgrade()
        {
            Upgrade();
        }
    }
}