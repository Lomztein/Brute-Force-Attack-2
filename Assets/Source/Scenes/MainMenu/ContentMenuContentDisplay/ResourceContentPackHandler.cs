using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.LocalizationSystem;
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
    public class ResourceContentPackHandler : ContentPackHandler<ResourcesContentPack>
    {
        public Sprite Sprite;
        public string Text;

        protected override void InstantiateElements(ResourcesContentPack pack, Transform parent)
        {
            (GameObject _, Image img, Text txt) = InstantiateElement(parent);
            img.sprite = Sprite;
            txt.text = Localization.Get(Text, Resources.FindObjectsOfTypeAll<Object>().Length);
        }
    }
}
