using Lomztein.BFA2.Content.Loaders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Lomztein.BFA2.Content
{
    public class ContentGroup : IContentGroup
    {
        private string _rootPath;

        private IRawContentLoader[] _loaders = new IRawContentLoader[]
        {
            new GameObjectModelRawContentLoader (),
            new Texture2DRawContentLoader (),
        };

        public object LoadContent(string path, Type type)
        {
            foreach (IRawContentLoader loader in _loaders)
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