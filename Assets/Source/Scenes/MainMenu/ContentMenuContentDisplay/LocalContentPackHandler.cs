using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.LocalizationSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.MainMenu.ContentMenuContentDisplay
{
    public class LocalContentPackHandler : ContentPackHandler<ContentPack>
    {
        public ContentDirectory[] DirectoriesToCheck;

        protected override void InstantiateElements(ContentPack pack, Transform parent)
        {
            foreach (ContentDirectory directory in DirectoriesToCheck)
            {
                string path = GetDirectoryPath(pack, directory);
                if (Directory.Exists(path))
                {
                    (GameObject go, Image img, Text text) = InstantiateElement(parent);
                    img.sprite = directory.Image;
                    text.text = Localization.Get(directory.Text, Directory.GetFiles(path).Where(x => !x.EndsWith(".meta")).Count());
                }
            }
        }

        string GetDirectoryPath (ContentPack pack, ContentDirectory directory)
        {
            return Path.Combine(pack.Path, directory.RelativePath);
        }

        public class ContentDirectory
        {
            public string RelativePath;
            public string Text;
            public Sprite Image;
        }
    }
}
