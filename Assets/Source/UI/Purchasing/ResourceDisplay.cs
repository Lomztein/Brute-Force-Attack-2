using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Purchasing.UI
{
    public class ResourceDisplay : MonoBehaviour
    {
        public GameObject ResourceContainer;
        private IResourceContainer _container;
        public Resource Resource;
        public Text Text;

        private void Awake()
        {
            _container = ResourceContainer.GetComponent<IResourceContainer>();
            _container.OnResourceChanged += OnResourceChanged;
        }

        private void Start()
        {
            OnResourceChanged(Resource, _container.GetResource(Resource), _container.GetResource(Resource));
        }

        private void OnResourceChanged(Resource resource, int prev, int cur)
        {
            if (resource.Equals(Resource))
            {
                UpdateDisplay(cur);
            }
        }

        private void UpdateDisplay(int amount)
        {
            Text.text = $"{(amount == int.MaxValue ? "Infinite" : amount.ToString())} {Resource.Name}";
        }
    }
}
