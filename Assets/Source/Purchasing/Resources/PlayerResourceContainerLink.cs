using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    public class PlayerResourceContainerLink : MonoBehaviour, IResourceContainer
    {
        private IResourceContainer _internalContainer;
        private const string PLAYER_CONTAINER_OBJECT_TAG = "PlayerResourceContainer";

        private void Start()
        {
            _internalContainer = GameObject.FindGameObjectWithTag(PLAYER_CONTAINER_OBJECT_TAG).GetComponent<IResourceContainer>();
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