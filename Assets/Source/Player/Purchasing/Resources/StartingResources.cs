using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public class StartingResources : MonoBehaviour
    {
        public ResourceCost Resources;

        private void Start()
        {
            IResourceContainer container = GetComponent<IResourceContainer>();

            foreach (var resource in Resources.GetCost())
            {
                container.ChangeResource(resource.Key, resource.Value);
            }
            Destroy(this);
        }
    }
}
