using Lomztein.BFA2.ContentSystem.Loaders;
using Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders;
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
    public class ContentPack : IContentPack
    {
        private const string IGNORE_PREFIX = "IGNORE_"; // Any files prefixed with this will be ignored.
        private const string IGNORE_SUFFIX = ".meta"; // Any files suffixed with this will be ignored.
        private const string ASSET_BUNDLE_RELATIVE_PATH = "Assets";
        private const string PLUGINS_RELATIVE_PATH = "Plugins";

        public readonly string Path;
        private string PluginDirectory => System.IO.Path.Combine(Path, PLUGINS_RELATIVE_PATH);

        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }
        public string Version { get; private set; }
        public Texture2D Image { get; private set; }
        public bool RequireReload => Directory.Exists(PluginDirectory);
        public bool HasAssetBundle => _includedAssets != null;

        private IContentLoader _contentLoader = new ContentLoader();
        private AssetBundleContentPack _includedAssets;

        public ContentPack(string path, string name, string author, string description, string version, Texture2D sprite)
        {
            Path = path;
            Name = name;
            Author = author;
            Description = description;
            Version = version;
            Image = sprite;
        }

        public void Init ()
        {
            _includedAssets = AssetBundleContentPack.FromFile(System.IO.Path.Combine(Path, $"{ASSET_BUNDLE_RELATIVE_PATH}.unity3d"));
        }

        private bool ShouldLoadFromBundle (string path)
            => path.StartsWith(ASSET_BUNDLE_RELATIVE_PATH) && _includedAssets != null;

        public object LoadContent(string path, Type asType)
        {
            if (ShouldLoadFromBundle(path))
            {
                return _includedAssets.LoadContent(path.Substring(ASSET_BUNDLE_RELATIVE_PATH.Length), asType);
            }
            else
            {
                string absolutePath = System.IO.Path.Combine(Path, path);
                try
                {
                    return _contentLoader.LoadContent(absolutePath, asType);
                }
                catch (Exception exc)
                {
                    Debug.LogException(exc);
                    Debug.LogWarning($"File '{absolutePath}' could not be loaded. See preceeding callstack for more info.");
                    return null;
                }
            }
        }

        public string[] GetContentPaths()
        {
            return Directory.GetFiles(Path, "*", SearchOption.AllDirectories)
                .Where(x => System.IO.Path.GetExtension(x) != IGNORE_SUFFIX)
                .Where(x => !System.IO.Path.GetFileName(x).StartsWith(IGNORE_PREFIX))
                .Select(x => x.Substring(Path.Length))
                .ToArray();
        }

        public override string ToString()
        {
            return Name;
        }

        public string[] GetPluginAssemblies ()
        {
            if (Directory.Exists(PluginDirectory))
            {
                return Directory.GetFiles(PluginDirectory, "*.dll");
            }
            else
            {
                return Array.Empty<string>();
            }
        }
    }
}
