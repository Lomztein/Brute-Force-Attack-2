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
    public class ResourceDisplay : MonoBehaviour, ITooltip
    {
        public GameObject ResourceContainer;

        private IResourceContainer _container;

        public Resource Resource;
        private ResourceInfo _info;

        public Text Text;

        public string Title => "Shorthand: " + _info?.Shorthand;
        public string Description => null;
        public string Footnote => null;

        private void Awake()
        {
            _container = ResourceContainer.GetComponent<IResourceContainer>();
            _container.OnResourceChanged += OnResourceChanged;
            _info = ResourceInfo.Get(Resource);

            OnResourceChanged(Resource, _container.GetResource(Resource), _container.GetResource(Resource));
        }

        private void OnResourceChanged(Resource resource, int prev, int cur)
        {
            if (resource == Resource)
            {
                UpdateDisplay(cur);
            }
        }

        private void UpdateDisplay(int amount)
        {
            Text.text = $"{(amount == int.MaxValue ? "Infinite" : amount.ToString())} {_info.Name}";
        }
    }
}
