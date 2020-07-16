using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.Objects
{
    public interface IContentCachedPrefab : IContentPrefab
    {
        GameObject GetCache();

        void Dispose();
    }
}
