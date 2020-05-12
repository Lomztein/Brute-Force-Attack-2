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
            return Resources.LoadAll(path, type);
        }

        public object GetContent(string path, Type type)
        {
            return Resources.Load(path, type);
        }
    }
}
