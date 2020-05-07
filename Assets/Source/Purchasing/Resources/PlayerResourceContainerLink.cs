using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public class PlayerResourceContainerLink : MonoBehaviour, IResourceContainer
    {
        private IResourceContainer _internalContainer;
        private const string PLAYER_RESOURCE_OBJECT_NAME = "PlayerResourceContainer";

        private void Awake()
        {
            _internalContainer = GameObject.Find(PLAYER_RESOURCE_OBJECT_NAME).GetComponent<IResourceContainer>();
        }

        public int GetResource(Resource resource)
        {
            return _internalContainer.GetResource(resource);
        }

        public void ChangeResource(Resource resource, int value)
        {
            _internalContainer.ChangeResource(resource, value);
        }

        public void SetResource(Resource resource, int value)
        {
            _internalContainer.SetResource(resource, value);
        }
    }
}