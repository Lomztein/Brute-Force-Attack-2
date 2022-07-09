using Lomztein.BFA2.ContentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Lomztein.BFA2.Scenes.MainMenu.ContentMenuContentDisplay
{
    public abstract class ContentPackHandler : MonoBehaviour
    {
        public GameObject ContentElementPrefab;

        public abstract bool CanHandle(Type contentPackType);

        public abstract void InstantiateElements(IContentPack pack, Transform parent);

        protected (GameObject go, Image img, Text txt) InstantiateElement(Transform parent)
        {
            GameObject gameObject = Object.Instantiate(ContentElementPrefab, parent);
            return (gameObject, gameObject.GetComponentInChildren<Image>(), gameObject.GetComponentInChildren<Text>());
        }
    }

    public abstract class ContentPackHandler<T> : ContentPackHandler where T : IContentPack
    {

        public override bool CanHandle(Type contentPackType)
            => typeof(T) == contentPackType;

        public override void InstantiateElements (IContentPack pack, Transform parent)
        {
            InstantiateElements((T)pack, parent);
        }

        protected abstract void InstantiateElements(T pack, Transform parent);
    }
}
