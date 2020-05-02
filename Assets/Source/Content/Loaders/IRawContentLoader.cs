using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders
{
    public interface IRawContentLoader
    {
        Type ContentType { get; }

        object Load(string path);
    }
}