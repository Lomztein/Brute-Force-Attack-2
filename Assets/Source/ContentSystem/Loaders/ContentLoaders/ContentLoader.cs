﻿using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class ContentLoader : IContentLoader
    {
        private static List<IContentLoaderStrategy> _loaders;

        public ContentLoader()
        {
            if (_loaders == null)
            {
                _loaders = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IContentLoaderStrategy>(typeof (FallbackLoaderStrategy)).ToList();
                _loaders.Add(new FallbackLoaderStrategy());
            }
        }

        public object LoadContent(string path, Type type, IEnumerable<string> patches)
        {
            var loader = _loaders.FirstOrDefault(x => x.CanLoad(type));
            if (loader != null)
            {
                return loader.Load(path, type, patches);
            }
            throw new NotImplementedException($"Failed to load object of {nameof(type)} {type.FullName}, no fitting RawContentTypeLoader available.");
        }
    }
}