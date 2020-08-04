using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public class PlayerResourceContainerLink : MonoBehaviour, IResourceContainer
    {
        private IResourceContainer _internalContainer;
        private const string PLAYER_CONTAINER_OBJECT_TAG = "PlayerResourceContainer";

        public event Action<Resource, int, int> OnResourceChanged {
            add {
                GetInternalContainer().OnResourceChanged += value;
            }

            remove {
                GetInternalContainer().OnResourceChanged -= value;
            }
        }

        private void Awake()
        {
        }

        private IResourceContainer GetInternalContainer ()
        {
            if (_internalContainer == null)
            {
                _internalContainer = GameObject.FindGameObjectWithTag(PLAYER_CONTAINER_OBJECT_TAG).GetComponent<IResourceContainer>();
            }
            return _internalContainer;
        }

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