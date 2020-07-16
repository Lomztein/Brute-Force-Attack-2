using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References.PrefabProviders;
using Lomztein.BFA2.Purchasing.UI;
using Lomztein.BFA2.UI.Menus;
using Lomztein.BFA2.UI.PickerMenu.Buttons;
using Lomztein.BFA2.UI.PickerMenu.PickHandlers;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.UI.PickerMenu
{
    public class PickMenu : MonoBehaviour, ITabMenuElement
    {
        private IContentCachedPrefab[] _pickables;
        public GameObject ButtonPrefab;
        public Transform ButtonParent;

        private IPickHandler _pickHandler;
        private ICachedPrefabProvider _prefabSource;

        public bool IsMenuEmpty => false;
        [SerializeField] private string _name;
        public string Name => _name;

        private void Awake()
        {
            _prefabSource = GetComponent<ICachedPrefabProvider>();
            _pickHandler = GetComponent<IPickHandler>();

            _pickables = _prefabSource.Get();
        }

        private void Start()
        {
            CreateButtons();
        }

        public void CreateButtons ()
        {
            ClearButtons();
            foreach (var pickable in _pickables)
            {
                GameObject newButton = Instantiate(ButtonPrefab, ButtonParent);
                IPickableButton button = newButton.GetComponent<IPickableButton>();
                button.Assign(pickable, () => HandlePurchase(pickable));
            }
        }

        private void HandlePurchase(IContentCachedPrefab prefab)
        {
            _pickHandler.Handle(prefab);
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
