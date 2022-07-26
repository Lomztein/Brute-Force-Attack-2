using System;
using System.Collections;
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
        public string Author => null; //TODO: Figure out a way to include information in the asset bundle. About.json in asset bundle?
        public string Version => null;
        public string Description => null;
        public bool RequireReload => true;
        public Texture2D Image => null;

        private AssetBundle _assetBundle;

        public AssetBundleContentPack(AssetBundle bundle)
        {
            _assetBundle = bundle;
        }

        public object LoadContent(string path, Type type)
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

        public static AssetBundleContentPack FromFile(string path)
        {
            if (File.Exists(path))
            {
                return new AssetBundleContentPack(AssetBundle.LoadFromFile(path));
            }
            return null;
        }

        public void Init () { }

        public string[] GetContentPaths()
        {
            return _assetBundle.GetAllAssetNames();
        }
    }
}
