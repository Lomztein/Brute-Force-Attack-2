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
            var values = Resource.GetResources();
            foreach (var value in values)
            {
                this.SetResource(value, 0);
            }
        }

        public int GetResource(Resource resource)
        {
            if (_resources.TryGetValue(resource, out int value))
            {
                return value;
            }
            return 0;
        }

        public void SetResource(Resource resource, int value, bool silent)
        {
            if (!_resources.ContainsKey(resource))
            {
                if (_resources != null)
                {
                    _resources.Add(resource, value);
                    if (!silent)
                    {
                        OnResourceChanged?.Invoke(resource, 0, _resources[resource]);
                    }
                }
                else
                {
                    throw new ArgumentException("Tried to spend a null resource.");
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
