using Lomztein.BFA2.Content.Loaders.TypeLoaders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            foreach (IRawContentTypeLoader loader in _loaders)
            {
                if (loader.ContentType == type)
                {
                    return loader.Load(path);
                }
                throw new NotImplementedException($"Failed to load object of {nameof(type)} {type.FullName}, no fitting RawContentTypeLoader available.");
            }
            throw new FileNotFoundException($"Could not load content file {path}, file not found.");
        }
    }
}