using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class RawContentLoader : IRawContentLoader
    {
        private static IRawContentTypeLoader[] _loaders;
        private static List<IRawContentTypeLoader> _customLoaders = new List<IRawContentTypeLoader>(); // Not currently used, perhaps split into different loader inside ContentPack.

        public static void AddCustomLoader(IRawContentTypeLoader loader) => _customLoaders.Add(loader);

        public RawContentLoader()
        {
            if (_loaders == null)
            {
                _loaders = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IRawContentTypeLoader>().ToArray();
            }
        }

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