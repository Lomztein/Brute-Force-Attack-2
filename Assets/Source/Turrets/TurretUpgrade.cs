using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers.Turret;
using Lomztein.BFA2.Turrets;
using Lomztein.BFA2.UI.Tooltip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour, ITooltip
{
    [ModelProperty]
    public string UpgradeComponentIdentifier;
    private IResourceContainer _resourceContainer;

    public string Text => $"{GetThis().Name} -> {GetUpgrade().Name}\n\t{GetUpgrade().Cost.ToString()}";

    private void Start()
    {
        _resourceContainer = GetComponent<IResourceContainer>();
    }

    public void OnMouseDown()
    {
        if (CanUpgrade())
        {
            Upgrade();
        }
    }

    private bool CanUpgrade ()
    {
        return _resourceContainer.HasEnough(GetUpgrade().Cost);
    }

    private void Upgrade ()
    {
        _resourceContainer.TrySpend(GetUpgrade().Cost);

        ITurretAssembly assembly = GetComponentInParent<ITurretAssembly>();
        ITurretComponent current = GetComponent<ITurretComponent>();

        assembly.RemoveComponent(current);

        GameObject newObj = GameObjectTurretComponentAssembler.GetComponent(UpgradeComponentIdentifier).Instantiate();

        newObj.transform.position = transform.position;
        newObj.transform.rotation = transform.rotation;
        newObj.transform.localScale = transform.localScale;

        newObj.transform.SetParent(transform.parent, true);
        assembly.AddComponent(newObj.GetComponent<ITurretComponent>());

        Destroy(gameObject);
    }

    private IPurchasable GetThis() => GetComponent<IPurchasable>();

    private IPurchasable GetUpgrade ()
    {
        return GameObjectTurretComponentAssembler.GetComponent(UpgradeComponentIdentifier).GetCache().GetComponent<IPurchasable>();
    }
}
