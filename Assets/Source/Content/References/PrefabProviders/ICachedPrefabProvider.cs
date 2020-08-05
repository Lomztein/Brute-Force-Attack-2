using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content.References.PrefabProviders
{
    public interface ICachedPrefabProvider : IProvider<IContentCachedPrefab>
    {
        IContentCachedPrefab[] Get();
    }
}
