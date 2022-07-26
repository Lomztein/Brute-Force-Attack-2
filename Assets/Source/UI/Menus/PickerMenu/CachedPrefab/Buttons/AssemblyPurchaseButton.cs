using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Research;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI.ToolTip;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab.Buttons
{
    public class AssemblyPurchaseButton : PurchaseButton
    {
        private IUnlockList UnlockList => Player.Player.Unlocks;
        public GameObject MissingResearchToolTip;

        public override GameObject GetToolTip()
        {
            if (IsAssemblyUnlocked())
            {
                return base.GetToolTip();
            }
            else
            {
                GameObject newToolTip = Instantiate(MissingResearchToolTip);
                newToolTip.GetComponent<IPurchasableToolTip>().Assign(_purchasable);
                return newToolTip;
            }
        }

        protected override void Start()
        {
            base.Start();
            if (ResearchController.Instance)
            {
                ResearchController.Instance.OnResearchCompleted += Instance_OnResearchCompleted;
            }
            if (!IsAssemblyUnlocked())
            {
                transform.SetAsLastSibling();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (ResearchController.Instance)
            {
                ResearchController.Instance.OnResearchCompleted -= Instance_OnResearchCompleted;
            }
        }

        private void Instance_OnResearchCompleted(ResearchOption obj)
        {
            UpdateInteractable();
        }

        protected override void UpdateInteractable()
        {
            base.UpdateInteractable();
            if (!IsAssemblyUnlocked())
            {
                Image.color = Color.black;
                Button.interactable = false;
            }
        }

        private bool IsAssemblyUnlocked ()
        {
            if (_prefab.GetCache().TryGetComponent(out TurretAssembly assembly))
            {
                return assembly.GetComponents().All(x => UnlockList.IsUnlocked(x.Identifier));
            }
            return false;
        }
    }
}
