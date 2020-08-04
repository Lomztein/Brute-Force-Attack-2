using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References.PrefabProviders;
using Lomztein.BFA2.UI.Menus.PickerMenu.Buttons;
using Lomztein.BFA2.UI.Menus.PickerMenu.PickHandlers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu
{
    public class PickMenu : MonoBehaviour, ITabMenuElement
    {
        private List<IContentCachedPrefab> _pickables = new List<IContentCachedPrefab>();
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
        }

        private void Start()
        {
            _pickables = _prefabSource.Get().ToList();
            CreateButtons();
        }

        public void AddPickables (IEnumerable<IContentCachedPrefab> pickables)
        {
            _pickables.AddRange(pickables);
            CreateButtons();
        }

        public void SetPickables (IContentCachedPrefab[] pickables)
        {
            _pickables = pickables.ToList();
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
