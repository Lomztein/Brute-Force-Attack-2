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

namespace Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab.Buttons
{
    public class PurchaseButton : MonoBehaviour, IPickableButton<IContentCachedPrefab>, ITooltip
    {
        protected IContentCachedPrefab _prefab;
        protected IPurchasable _purchasable;

        private Action _onSelected;
        private IResourceContainer _resourceContainer;

        public Button Button;
        public Image Image;

        public string Title => _purchasable.Name + " - " + _purchasable.Cost.Format();
        public string Description => _purchasable.Description;
        public string Footnote => string.Empty;

        private void Start()
        {
            Button.onClick.AddListener(() => HandlePurchase());

            _resourceContainer = GetComponent<IResourceContainer>();
            _resourceContainer.OnResourceChanged += OnResourceChanged;

            UpdateGraphics();
            UpdateInteractable();
        }

        private void OnDestroy()
        {
            _resourceContainer.OnResourceChanged -= OnResourceChanged;
        }

        private void OnResourceChanged(Resource arg1, int arg2, int arg3)
        {
            UpdateInteractable();
        }

        protected void UpdateGraphics ()
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

        protected void UpdateInteractable ()
        {
            bool interactable = IsInteractable();
            Button.interactable = interactable;
            Image.color = interactable ? Color.white : new Color(1f, 1f, 1f, 0.5f);
        }

        protected virtual bool IsInteractable ()
        {
            return _resourceContainer.HasEnough(_purchasable.Cost);
        }

        public void HandlePurchase()
        {
            _onSelected();
        }

        public void Assign(IContentCachedPrefab prefab, Action onSelected)
        {
            _prefab = prefab;
            _purchasable = _prefab.GetCache().GetComponent<IPurchasable>();
            _onSelected = onSelected;
            Init();
        }

        public virtual void Init() { }
    }
}
