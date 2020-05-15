using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content.References.GameObjects.PrefabProviders
{
    public interface IPrefabProvider
    {
        GameObjectPrefab[] Get();
    }
}
