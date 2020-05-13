using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content
{
    public class ContentManager : MonoBehaviour, IContentManager
    {
        IContentPackSource _source = new ContentPackSource();

        private const string WILDCARD = "*";
        private IContentPack[] _packs;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _packs = _source.GetPacks();
            var content = GetAllContent("*/Assemblies/", typeof(IGameObjectModel));
        }

        public object GetContent(string path, Type type)
        {
            return GetPack(GetPackFolder(path)).GetContent(GetContentPath(path), type);
        }

        private string GetPackFolder(string path)
        {
            return path.Split('/').First();
        }

        private string GetContentPath(string path)
        {
            return path.Substring(path.IndexOf('/')+1);
        }

        public object[] GetAllContent(string path, Type type)
        {
            string packFolder = GetPackFolder(path);
            bool wildcard = packFolder == WILDCARD;

            string contentPath = GetContentPath(path);

            if (wildcard)
            {
                List<object> objects = new List<object>();
                foreach (IContentPack pack in _packs)
                {
                    objects.AddRange(pack.GetAllContent(contentPath, type));
                }
                return objects.ToArray();
            }
            else
            {
                return GetPack(packFolder).GetAllContent(contentPath, type);
            }
        }

        private IContentPack GetPack(string name) => _packs.FirstOrDefault(x => x.Name == name);


    }
}
