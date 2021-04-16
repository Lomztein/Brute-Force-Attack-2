using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public class ResourceContainer : MonoBehaviour, IResourceContainer
    {
        private Dictionary<Resource, int> _resources = new Dictionary<Resource, int>();

        public event Action<Resource, int, int> OnResourceChanged;

        private void Awake()
        {
            var values = Enum.GetNames(typeof(Resource));
            foreach (var value in values)
            {
                this.SetResource((Resource)Enum.Parse(typeof(Resource), value), 0);
            }
        }

        public int GetResource(Resource resource)
        {
            return _resources.ContainsKey(resource) ? _resources[resource] : 0;
        }

        public void SetResource(Resource resource, int value, bool silent)
        {
            if (!_resources.ContainsKey(resource))
            {
                _resources.Add(resource, value);
                if (!silent)
                {
                    OnResourceChanged?.Invoke(resource, 0, _resources[resource]);
                }
            }
            else
            {
                int before = _resources[resource];
                _resources[resource] = value;

                if (!silent)
                {
                    OnResourceChanged?.Invoke(resource, before, _resources[resource]);
                }
            }

        }
    }
}
