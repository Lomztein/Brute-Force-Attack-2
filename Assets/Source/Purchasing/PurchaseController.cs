using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.PurchaseHandlers;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Purchasing.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA.Purchasing
{
    public class PurchaseController : MonoBehaviour
    {
        public GameObject[] Purchasables;
        public GameObject ButtonPrefab;
        public Transform ButtonParent;

        private IPurchasableCollection _purchaseableCollection;
        private IPurchaseHandler _purchaseHandler;
        private IResourceContainer _resourceContainer;

        private void Awake()
        {
            _purchaseableCollection = new PurchasableCollection(Purchasables.Select(x => x.GetComponent<IPurchasable>()));
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
            if (_resourceContainer.TrySpend (purchasable.Cost))
            {
                _purchaseHandler.Handle(purchasable);
            }
        }

        private void ClearButtons ()
        {
            foreach (Transform child in ButtonParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
