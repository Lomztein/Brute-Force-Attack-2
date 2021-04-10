using Lomztein.BFA2.ContentSystem.Loaders;
using Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders;
using System;
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
        private const string IGNORE_PREFIX = "IGNORE_"; // Any files suffixed with this will be ignored.
        private const string JSON_FILE_EXTENSION = ".json"; // Any files prefixed with this will be ignored.
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
            => Path.StartsWith(ASSET_BUNDLE_RELATIVE_PATH) && _includedAssets != null;

        public object GetContent(string path, Type type)
        {
            if (ShouldLoadFromBundle(path))
            {
                return _includedAssets.GetContent(path.Substring(ASSET_BUNDLE_RELATIVE_PATH.Length), type);
            }
            else
            {
                return _contentLoader.LoadContent(System.IO.Path.Combine(Path, path), type);
            }
        }

        public object[] GetAllContent(string path, Type type)
        {
            if (ShouldLoadFromBundle(path))
            {
                return _includedAssets.GetAllContent(path.Substring(ASSET_BUNDLE_RELATIVE_PATH.Length), type);
            }

            List<object> content = new List<object>();
            string spath = System.IO.Path.Combine (Path, path);

            if (Directory.Exists(spath))
            {
                string[] files = Directory.GetFiles(spath, $"*{JSON_FILE_EXTENSION}");
                foreach (string file in files)
                {
                    if (!System.IO.Path.GetFileName(file).StartsWith(IGNORE_PREFIX))
                    {
                        try
                        {
                            var loaded = _contentLoader.LoadContent(file, type);
                            content.Add(loaded);
                        }catch (Exception exc)
                        {
                            Debug.LogException(exc);
                            Debug.LogWarning($"File '{file}' could not be loaded. See preceeding callstack for more info.");
                        }
                    }
                }
            }

            return content.ToArray();
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
