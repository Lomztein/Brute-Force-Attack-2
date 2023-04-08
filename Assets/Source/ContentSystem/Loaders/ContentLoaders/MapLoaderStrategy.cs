using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class MapLoaderStrategy : IContentLoaderStrategy
    {
        private const string PREVIEW_SUBFOLDER = "Previews";

        private FallbackLoaderStrategy _mapLoader = new FallbackLoaderStrategy();
        private Texture2DLoaderStrategy _textureLoader = new Texture2DLoaderStrategy();

        public bool CanLoad(Type type) => type == typeof(MapData);

        public object Load(string path, Type type, IEnumerable<string> patches)
        {
            string folder = Path.GetDirectoryName(path);
            string previewFolder = Path.Combine(folder, PREVIEW_SUBFOLDER);

            MapData data = (MapData)_mapLoader.Load(path, type, patches);
            string previewPath = Path.Combine(previewFolder, data.Name + ".png");
            if (data.Preview == null && File.Exists(previewPath))
            {
                Texture2D preview = (Texture2D)_textureLoader.Load(previewPath, typeof(Texture2D), Array.Empty<string>());
                data.Preview = preview;
            }

            return data;
        }
    }
}
