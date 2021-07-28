using Lomztein.BFA2.Misc;
using System;
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
        public int Count => _pickables.Count;

        private IPickHandler<T> _pickHandler;
        private IProvider<T> _provider;

        public bool IsMenuEmpty => _pickables.Any();
        public string _Name;

        public event Action<ITabMenuElement> OnNameChanged;
        
        public string Name => $"{_Name} ({Count})";
        private bool _isInitted;


        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (!_isInitted)
            {
                _provider = GetComponent<IProvider<T>>();
                _pickHandler = GetComponent<IPickHandler<T>>();

                RegisterDynamicProvider();
                OnNameChanged?.Invoke(this);

                _isInitted = true;

                if (_provider != null)
                {
                    SetPickables(_provider.Get());
                }
            }
        }

        private void RegisterDynamicProvider ()
        {
            if (_provider != null && _provider is IDynamicProvider<T> dyn)
            {
                dyn.OnAdded += OnAdded;
                dyn.OnRemoved += OnRemoved;
            }
        }

        private void OnAdded(IEnumerable<T> objs)
        {
            AddPickables(objs);
        }

        private void OnRemoved(IEnumerable<T> objs)
        {
            RemovePickables(objs);
        }

        public virtual void AddPickables (IEnumerable<T> pickables)
        {
            _pickables.AddRange(pickables);
            UpdateButtons();
            OnNameChanged?.Invoke(this);
        }

        public virtual void RemovePickables (IEnumerable<T> pickables)
        {
            _pickables.RemoveAll(x => pickables.Contains(x));
            UpdateButtons();
            OnNameChanged?.Invoke(this);
        }

        public virtual void SetPickables (IEnumerable<T> pickables)
        {
            _pickables = pickables.ToList();
            UpdateButtons();
            OnNameChanged?.Invoke(this);
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
