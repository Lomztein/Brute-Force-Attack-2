using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public interface IContentLoaderStrategy
    {
        bool CanLoad(Type type);

        object Load(string path, Type type, IEnumerable<string> patches);
    }
}