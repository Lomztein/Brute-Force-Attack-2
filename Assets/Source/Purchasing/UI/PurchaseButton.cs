using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Purchasing.UI
{
    public class PurchaseButton : MonoBehaviour, IPurchaseButton
    {
        private IPurchasable _purchasable;
        private Action _onSelected;

        public Button Button;
        public Text Text;
        public Image Image;

        private void Awake()
        {
            Button.onClick.AddListener(() => HandlePurchase());
        }

        private void UpdateGraphics ()
        {
            if (Text)
            {
                Text.text = _purchasable.Name + " - " + string.Join(", ", _purchasable.Cost.GetCost().Select(x => $"{ResourceInfo.Get(x.Key).Shorthand}: {x.Value}"));
            }
            if (Image)
            {
                Image.sprite = _purchasable.Sprite;
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
