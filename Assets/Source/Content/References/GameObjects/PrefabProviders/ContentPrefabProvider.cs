using Lomztein.BFA2.Serialization.Models.GameObject;
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

        public ContentGameObject[] Get()
            => Content.GetAll(Path, typeof(IGameObjectModel)).Select(x => new ContentGameObject(x as IGameObjectModel)).ToArray();
    }
}
