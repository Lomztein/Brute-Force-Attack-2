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

        private readonly string _path;
        private string PluginDirectory => Path.Combine(_path, PLUGINS_RELATIVE_PATH);

        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }
        public bool RequireReload => Directory.Exists(PluginDirectory);

        private IContentLoader _contentLoader = new ContentLoader();
        private AssetBundleContentPack _includedAssets;

        public ContentPack(string path, string name, string author, string description)
        {
            _path = path;
            Name = name;
            Author = author;
            Description = description;
        }

        public void Init ()
        {
            _includedAssets = AssetBundleContentPack.FromFile(Path.Combine(_path, $"{ASSET_BUNDLE_RELATIVE_PATH}.unity3d"));
        }

        private bool ShouldLoadFromBundle (string path)
            => _path.StartsWith(ASSET_BUNDLE_RELATIVE_PATH) && _includedAssets != null;

        public object GetContent(string path, Type type)
        {
            if (ShouldLoadFromBundle(path))
            {
                return _includedAssets.GetContent(path.Substring(ASSET_BUNDLE_RELATIVE_PATH.Length), type);
            }
            else
            {
                return _contentLoader.LoadContent(Path.Combine(_path, path), type);
            }
        }

        public object[] GetAllContent(string path, Type type)
        {
            if (ShouldLoadFromBundle(path))
            {
                return _includedAssets.GetAllContent(path.Substring(ASSET_BUNDLE_RELATIVE_PATH.Length), type);
            }

            List<object> content = new List<object>();
            string spath = Path.Combine (_path, path);

            if (Directory.Exists(spath))
            {
                string[] files = Directory.GetFiles(spath, $"*{JSON_FILE_EXTENSION}");
                foreach (string file in files)
                {
                    if (!Path.GetFileName(file).StartsWith(IGNORE_PREFIX))
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
