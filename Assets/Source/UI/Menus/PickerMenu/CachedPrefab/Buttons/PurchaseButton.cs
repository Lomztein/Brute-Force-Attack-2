using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab.Buttons
{
    public class PurchaseButton : MonoBehaviour, IPickableButton<IContentCachedPrefab>, IHasToolTip
    {
        protected IContentCachedPrefab _prefab;
        protected IPurchasable _purchasable;

        private Action _onSelected;
        private IResourceContainer _resourceContainer;

        public Button Button;
        public Image Image;

        public GameObject ToolTipPrefab;

        public virtual GameObject GetToolTip()
        {
            GameObject newToolTip = Instantiate(ToolTipPrefab);
            newToolTip.GetComponent<IPurchasableToolTip>().Assign(_purchasable);
            return newToolTip;
        }

        protected virtual void Start()
        {
            Button.onClick.AddListener(() => HandlePurchase());

            _resourceContainer = GetComponent<IResourceContainer>();
            _resourceContainer.OnResourceChanged += OnResourceChanged;

            UpdateGraphics();
            UpdateInteractable();
        }

        protected virtual void OnDestroy()
        {
            if (_resourceContainer != null)
            {
                _resourceContainer.OnResourceChanged -= OnResourceChanged;
            }
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

        protected virtual void UpdateInteractable ()
        {
            bool interactable = _resourceContainer.HasEnough(_purchasable.Cost);
            Button.interactable = interactable;
            Image.color = interactable ? Color.white : new Color(1f, 1f, 1f, 0.5f);
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
