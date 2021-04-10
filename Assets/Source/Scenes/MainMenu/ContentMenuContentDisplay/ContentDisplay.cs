using Lomztein.BFA2.ContentSystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.MainMenu.ContentMenuContentDisplay
{
    public class ContentDisplay : SerializedMonoBehaviour
    {
        public List<ContentPackHandler> Handlers;
        public Transform ListParent;

        private ContentPackHandler FindHandler (IContentPack pack)
        {
            return Handlers.FirstOrDefault(x => x.CanHandle(pack.GetType()));
        }

        public void DisplayContent (IContentPack contentPack)
        {
            Clear();
            ContentPackHandler handler = FindHandler(contentPack);
            if (handler != null)
            {
                handler.InstantiateElements(contentPack, ListParent);
            }
        }

        public void Clear ()
        {
            foreach (Transform child in ListParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
