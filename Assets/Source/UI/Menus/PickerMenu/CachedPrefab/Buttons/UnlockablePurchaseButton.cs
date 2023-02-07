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
    public class UnlockablePurchaseButton : PurchaseButton
    {
        protected IUnlockList UnlockList => Player.Player.Unlocks;
        public GameObject MissingResearchToolTip;

        public override GameObject InstantiateToolTip()
        {
            if (IsUnlocked())
            {
                return base.InstantiateToolTip();
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
            if (!IsUnlocked())
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
            if (!IsUnlocked())
            {
                Image.color = Color.black;
                Button.interactable = false;
            }
        }

        protected virtual bool IsUnlocked ()
        {
            if (_prefab.GetCache().TryGetComponent(out IIdentifiable identifiable))
            {
                return UnlockList.IsUnlocked(identifiable.Identifier);
            }
            return false;
        }
    }
}
