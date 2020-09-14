using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Content.References
{
    [Serializable]
    public class ContentMaterialReference
    {
        [ModelProperty]
        public string Path;
        private Material _cache;

        public Material GetMaterial ()
        {
            if (_cache == null)
            {
                _cache = Content.Get(Path, typeof(Material)) as Material;
            }

            return _cache;
        }
    }
}
