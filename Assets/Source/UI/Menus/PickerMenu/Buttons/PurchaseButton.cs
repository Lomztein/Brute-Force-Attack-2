using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.PickerMenu.Buttons
{
    public class PurchaseButton : MonoBehaviour, IPickableButton, ITooltip
    {
        private IPurchasable _purchasable;
        private Action _onSelected;

        public Button Button;
        public Image Image;

        public string Title => _purchasable.Name + " - " + _purchasable.Cost.Format();
        public string Description => _purchasable.Description;
        public string Footnote => string.Empty;

        private void Awake()
        {
            Button.onClick.AddListener(() => HandlePurchase());
        }

        private void UpdateGraphics ()
        {
            if (Image)
            {
                Image.sprite = _purchasable.Sprite;
                if (Image.sprite == null)
                {
                    Image.enabled = false;
                }
            }
        }

        public void HandlePurchase()
        {
            _onSelected();
        }

        public void Assign(IContentCachedPrefab prefab, Action onSelected)
        {
            _purchasable = prefab.GetCache().GetComponent<IPurchasable>();
            _onSelected = onSelected;
            UpdateGraphics();
        }
    }
}
