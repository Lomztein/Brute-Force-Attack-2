using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.ContentSystem.References.PrefabProviders
{
    public interface ICachedPrefabProvider : IProvider<IContentCachedPrefab>
    {
        IContentCachedPrefab[] Get();
    }
}
