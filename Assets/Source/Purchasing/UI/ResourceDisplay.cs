using Lomztein.BFA2.Purchasing.Resources;
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
        private ResourceInfo _info;

        public Text Text;

        private void Awake()
        {
            _container = ResourceContainer.GetComponent<IResourceContainer>();
            _info = ResourceInfo.Get(Resource);
        }

        private void Update()
        {
            int amount = _container.GetResource(Resource);
            Text.text = $"{(amount == int.MaxValue ? "Infinite" : amount.ToString())} {_info.Shorthand}";
        }
    }
}
