using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders.ContentLoaders
{
    public interface IRawContentLoader
    {
        object LoadContent(string path, Type type);
    }
}