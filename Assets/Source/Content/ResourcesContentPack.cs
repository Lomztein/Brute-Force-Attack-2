using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content
{
    public class ResourcesContentPack : IContentPack
    {
        public string Name => "Resources";
        public string Author => "Brute Force Attack 2";
        public string Description => "Built-in resources.";

        public string Path => throw new NotImplementedException();

        public object[] GetAllContent(string path, Type type)
        {
            return Resources.LoadAll(path, type).Select (x => UnityEngine.Object.Instantiate(x)).ToArray();
        }

        public object GetContent(string path, Type type)
        {
            return UnityEngine.Object.Instantiate(Resources.Load(path, type));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
