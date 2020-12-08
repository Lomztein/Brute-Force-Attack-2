using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class AssetBundleContentPack : IContentPack
    {
        public string Name => _assetBundle.name;
        public string Author => null; //TODO: Figure out a way to include name, author, and description in the asset bundle. Text files perhaps?
        public string Description => null;

        private AssetBundle _assetBundle;
        private string[] _assetPaths;

        public AssetBundleContentPack(AssetBundle bundle)
        {
            _assetBundle = bundle;
        }

        public object[] GetAllContent(string path, Type type)
        {
            return _assetPaths.Where(x => x.StartsWith(path)).Select(x => GetContent(x, type)).Where(x => x != null).ToArray();
        }

        public object GetContent(string path, Type type)
        {
            if (_assetBundle.Contains(path))
            {
                return _assetBundle.LoadAsset(path, type);
            }
            else
            {
                Debug.LogError($"Unable to load asset '{path}' from AssetBundle '{Name}'.");
                return null;
            }
        }

        public void Init()
        {
            _assetPaths = _assetBundle.GetAllAssetNames();
        }

        public static AssetBundleContentPack FromFile(string path)
        {
            if (File.Exists(path))
            {
                return new AssetBundleContentPack(AssetBundle.LoadFromFile(path));
            }
            return null;
        }
    }
}
