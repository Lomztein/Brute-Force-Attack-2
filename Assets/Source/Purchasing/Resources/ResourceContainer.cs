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

        private void Awake()
        {
            var values = Enum.GetNames(typeof(Resource));
            foreach (var value in values)
            {
                _resources.Add((Resource)Enum.Parse(typeof(Resource), value), 0);
            }
        }

        public void ChangeResource(Resource resource, int value)
        {
            _resources[resource] += value;
        }

        public int GetResource(Resource resource)
        {
            return _resources[resource];
        }

        public void SetResource(Resource resource, int value)
        {
            _resources[resource] = value;
        }
    }
}
