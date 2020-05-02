using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders.TypeLoaders
{
    public interface IRawContentTypeLoader
    {
        Type ContentType { get; }

        object Load(string path);
    }
}