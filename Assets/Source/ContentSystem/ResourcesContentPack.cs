using Lomztein.BFA2.ContentSystem.Loaders.ResourceLoaders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lomztein.BFA2.ContentSystem
{
    public class ResourcesContentPack : IContentPack
    {
        public string Name => "Resources";
        public string Author => "Brute Force Attack 2";
        public string Description => "Built-in resources.";
        public bool RequireReload => false;

        public string Path => "Internal built-in resources.";

        public string Version => Application.version;
        public Texture2D Image => Resources.Load<Sprite>("ResourceContentPackSprite").texture;

        private IResourceTypeConverter[] _converters = new IResourceTypeConverter[]
        {
            new GameObjectToCachedGameObjectResourceConverter(),
        };

        public object LoadContent(string path, Type type, IEnumerable<string> patches)
        {
            Assert.IsTrue(patches.Count() == 0, "Cannot patch built-in resources.");
            var converter = GetConverter(type);

            if (converter == null)
            {
                return Resources.Load(path, type);
            }
            else
            {
                return converter.Convert (Resources.Load(path, converter.InputType));
            }
        }

        public IEnumerable<string> GetContentPaths()
        {
            return ContentManifest.Load().StartsWith("Resources", true).Select(x => System.IO.Path.ChangeExtension(x, null));
        }

        public IEnumerable<ContentOverride> GetContentOverrides()
            => ContentPackUtils.GetOverridesFromContentPaths(this);

        public IEnumerable<ContentPatch> GetContentPatches()
            => ContentPackUtils.GetPatchesFromContentPaths(this);

        public override string ToString()
        {
            return Name;
        }

        private IResourceTypeConverter GetConverter (Type outputType)
        {
            return _converters.FirstOrDefault(x => x.OutputType == outputType);
        }

        public void Init()
        {
        }
    }
}
