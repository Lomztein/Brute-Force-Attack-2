using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content
{
    public interface IContentGroup
    {
        object LoadContent(string path, Type type);
    }
}