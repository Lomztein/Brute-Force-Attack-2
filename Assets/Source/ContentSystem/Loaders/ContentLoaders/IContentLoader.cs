using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public interface IContentLoader
    {
        object LoadContent(string path, Type type, IEnumerable<string> patches);
    }
}