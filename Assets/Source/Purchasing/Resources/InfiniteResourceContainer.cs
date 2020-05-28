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
        public void ChangeResource(Resource resource, int value)
        {
        }

        public int GetResource(Resource resource)
        {
            return int.MaxValue;
        }

        public void SetResource(Resource resource, int value)
        {
        }
    }
}
