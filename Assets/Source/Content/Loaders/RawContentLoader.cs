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
        private string _rootPath;

        private readonly IRawContentTypeLoader[] _loaders = new IRawContentTypeLoader[]
        {
            new GameObjectModelRawContentLoader (),
            new Texture2DRawContentLoader (),
        };

        public object LoadContent(string path, Type type)
        {
            foreach (IRawContentTypeLoader loader in _loaders)
            {
                if (loader.ContentType == type)
                {
                    return loader.Load(Path.Combine(_rootPath, path));
                }
            }
            throw new FileNotFoundException($"Could not load content file {path}, file not found.");
        }
    }
}