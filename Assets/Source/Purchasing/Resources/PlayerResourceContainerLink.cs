using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public class PlayerResourceContainerLink : MonoBehaviour, IResourceContainer
    {
        public event Action<Resource, int, int> OnResourceChanged {
            add {
                GetInternalContainer().OnResourceChanged += value;
            }

            remove {
                GetInternalContainer().OnResourceChanged -= value;
            }
        }

        private IResourceContainer GetInternalContainer() => Player.Player.Resources;

        public int GetResource(Resource resource)
        {
            return GetInternalContainer().GetResource(resource);
        }

        public void ChangeResource(Resource resource, int value)
        {
            GetInternalContainer().ChangeResource(resource, value);
        }

        public void SetResource(Resource resource, int value)
        {
            GetInternalContainer().SetResource(resource, value);
        }
    }
}