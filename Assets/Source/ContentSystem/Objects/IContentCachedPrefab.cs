﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Objects
{
    public interface IContentCachedPrefab : IContentPrefab, IDisposableContent
    {
        GameObject GetCache();
    }
}
