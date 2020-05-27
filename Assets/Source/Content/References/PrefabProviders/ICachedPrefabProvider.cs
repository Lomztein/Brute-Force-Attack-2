using Lomztein.BFA2.Content.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content.References.PrefabProviders
{
    public interface ICachedPrefabProvider
    {
        IContentCachedPrefab[] Get();
    }
}
