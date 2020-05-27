using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Purchasing.UI
{
    public class PurchaseButton : MonoBehaviour, IPurchaseButton, ITooltip
    {
        private IPurchasable _purchasable;
        private Action _onSelected;

        public Button Button;
        public Image Image;

        public string Text => $"<b>{_purchasable.Name}</b> - <i>{_purchasable.Description}</i>\n\t{_purchasable.Cost.ToString()}";

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

        public void Assign(IPurchasable purchasable, Action onSelected)
        {
            _purchasable = purchasable;
            _onSelected = onSelected;
            UpdateGraphics();
        }
    }
}
