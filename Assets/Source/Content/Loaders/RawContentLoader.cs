using Lomztein.BFA2.Content.Loaders.TypeLoaders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders
{
    public class RawContentLoader : IRawContentLoader
    {
        private static readonly List<IRawContentTypeLoader> _loaders = new List<IRawContentTypeLoader>
        {
            new GameObjectModelRawContentLoader (),
            new Texture2DRawContentLoader (),
        };

        public static void AddLoader(IRawContentTypeLoader loader) => _loaders.Add(loader);

        public object LoadContent(string path, Type type)
        {
            if (File.Exists(path))
            {
                var loader = _loaders.FirstOrDefault(x => x.ContentType == type);
                if (loader != null)
                {
                    return loader.Load(path);
                }
                throw new NotImplementedException($"Failed to load object of {nameof(type)} {type.FullName}, no fitting RawContentTypeLoader available.");
            }
            else
            {
                throw new FileNotFoundException($"Could not load content file {path}, file not found.");
            }
        }
    }
}