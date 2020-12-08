using Lomztein.BFA2.ContentSystem.Loaders.ResourceLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ResourcesContentPack : IContentPack
    {
        public string Name => "Resources";
        public string Author => "Brute Force Attack 2";
        public string Description => "Built-in resources.";

        public string Path => "Internal built-in resources.";

        private IResourceTypeConverter[] _converters = new IResourceTypeConverter[]
        {
            new GameObjectToCachedGameObjectResourceConverter(),
        };

        public object[] GetAllContent(string path, Type type)
        {
            var converter = GetConverter(type);

            if (converter == null)
            {
                return Resources.LoadAll(path, type);
            }
            else
            {
                return Resources.LoadAll(path, converter.InputType).Select (x => converter.Convert(x)).ToArray();
            }
        }

        public object GetContent(string path, Type type)
        {
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
