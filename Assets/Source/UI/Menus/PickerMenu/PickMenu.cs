using Lomztein.BFA2.Misc;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu
{
    public abstract class PickMenu<T> : MonoBehaviour, ITabMenuElement
    {
        private List<T> _pickables = new List<T>();
        public GameObject ButtonPrefab;
        public Transform ButtonParent;

        private IPickHandler<T> _pickHandler;
        private IProvider<T> _provider;

        public bool IsMenuEmpty => _pickables.Any();
        [SerializeField] private string _name;
        public string Name => _name;

        private void Awake()
        {
            _provider = GetComponent<IProvider<T>>();
            _pickHandler = GetComponent<IPickHandler<T>>();

            RegisterDynamicProvider();
        }

        private void RegisterDynamicProvider ()
        {
            if (_provider is IDynamicProvider<T> dyn)
            {
                dyn.OnAdded += OnAdded;
                dyn.OnRemoved += OnRemoved;
            }
        }

        private void OnAdded(T obj)
        {
            AddPickables(new[] { obj });
        }

        private void OnRemoved(T obj)
        {
            RemovePickables(new[] { obj });
        }

        private void Start()
        {
            SetPickables(_provider.Get());
        }

        public virtual void AddPickables (IEnumerable<T> pickables)
        {
            _pickables.AddRange(pickables);
            UpdateButtons();
        }

        public virtual void RemovePickables (IEnumerable<T> pickables)
        {
            _pickables.RemoveAll(x => pickables.Contains(x));
            UpdateButtons();
        }

        public virtual void SetPickables (IEnumerable<T> pickables)
        {
            _pickables = pickables.ToList();
            UpdateButtons();
        }

        protected void UpdateButtons ()
        {
            ClearButtons();
            foreach (var pickable in _pickables)
            {
                GameObject newButton = Instantiate(ButtonPrefab, ButtonParent);
                IPickableButton<T> button = newButton.GetComponent<IPickableButton<T>>();
                button.Assign(pickable, () => HandlePurchase(pickable));
            }
        }

        private void HandlePurchase(T prefab)
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
