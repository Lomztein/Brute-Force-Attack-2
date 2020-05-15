using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Content.References.GameObjects;
using Lomztein.BFA2.Content.References.GameObjects.PrefabProviders;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.PurchaseHandlers;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Purchasing.UI;
using Lomztein.BFA2.UI.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing
{
    public class PurchaseMenu : MonoBehaviour, ITabMenuElement
    {
        private CachedGameObject[] _purchasables;
        public GameObject ButtonPrefab;
        public Transform ButtonParent;

        private IPurchasableCollection _purchaseableCollection;
        private IPurchaseHandler _purchaseHandler;
        private IResourceContainer _resourceContainer;
        private IPrefabProvider _prefabSource;

        public bool IsMenuEmpty => false;
        [SerializeField] private string _name;
        public string Name => _name;

        private void Awake()
        {
            _prefabSource = GetComponent<IPrefabProvider>();
            _purchasables = _prefabSource.Get().Select (x => new CachedGameObject(x)).ToArray();

            _purchaseableCollection = new PurchasableCollection(_purchasables.Select(x => x.Get().GetComponent<IPurchasable>()));
            _purchaseHandler = GetComponent<IPurchaseHandler>();
            _resourceContainer = GetComponent<IResourceContainer>();
            CreateButtons();
        }

        public void CreateButtons ()
        {
            ClearButtons();
            IPurchasable[] purchasables = _purchaseableCollection.GetPurchasables();
            foreach (var purchasable in purchasables)
            {
                GameObject newButton = Instantiate(ButtonPrefab, ButtonParent);
                IPurchaseButton button = newButton.GetComponent<IPurchaseButton>();
                button.Assign(purchasable, () => HandlePurchase(purchasable));
            }
        }

        private void HandlePurchase (IPurchasable purchasable)
        {
            if (_resourceContainer.HasEnough (purchasable.Cost))
            {
                _purchaseHandler.Handle(purchasable, _resourceContainer);
            }
        }

        private void ClearButtons ()
        {
            foreach (Transform child in ButtonParent)
            {
                Destroy(child.gameObject);
            }
        }

        public void OpenMenu()
        {
            gameObject.SetActive(true);
        }

        public void CloseMenu()
        {
            gameObject.SetActive(false);
        }
    }
}
