using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    class InfiniteResourceContainer : MonoBehaviour, IResourceContainer
    {
        public event Action<Resource, int, int> OnResourceChanged;

        public void ChangeResource(Resource resource, int value)
        {
            SetResource(resource, value);
        }

        public int GetResource(Resource resource)
        {
            return int.MaxValue;
        }

        public void SetResource(Resource resource, int value)
        {
            OnResourceChanged?.Invoke(resource, int.MaxValue, int.MaxValue);
        }
    }
}
