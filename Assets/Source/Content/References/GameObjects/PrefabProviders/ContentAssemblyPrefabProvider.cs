using Lomztein.BFA2.Serialization.Models.Turret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.GameObjects.PrefabProviders
{
    public class ContentAssemblyPrefabProvider : MonoBehaviour, IPrefabProvider
    {
        public string Path;

        public IContentGameObject[] Get()
            => Content.GetAll(Path, typeof(ITurretAssemblyModel)).Select(x => new ContentTurretAssemblyModel(x as ITurretAssemblyModel)).ToArray();
    }
}
