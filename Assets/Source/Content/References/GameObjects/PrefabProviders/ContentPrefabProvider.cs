using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.GameObjects.PrefabProviders
{
    public class ContentPrefabProvider : MonoBehaviour, IPrefabProvider
    {
        public string Path;

        public GameObjectPrefab[] Get()
            => Content.GetAll(Path, typeof(GameObject)).Select(x => new GameObjectPrefab(x as GameObject, false)).ToArray();
    }
}
