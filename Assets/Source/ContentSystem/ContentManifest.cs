using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentManifest : IEnumerable<string>
    {
        public static string PATH => Path.Combine(Application.streamingAssetsPath, "Manifest.txt");

        private List<string> _files = new List<string>();
        public IEnumerable<string> Files => _files;

        public ContentManifest() { }

        public ContentManifest(IEnumerable<string> manifest)
        {
            _files = manifest.ToList();
        }

        public IEnumerable<string> StartsWith(string startsWith, bool returnSubstring)
        {
            var files = Files.Where(x => x.StartsWith(startsWith));
            if (returnSubstring) return files.Select(x => x.Substring(startsWith.Length + 1));
            return files;
        }

        public static ContentManifest BuildFromContentPacks()
        {
            List<string> packsToBuild = new List<string>()
            {
                Path.Combine(Paths.Data, "Resources")
            };
            string contentRoot = Path.Combine(Paths.StreamingAssets, "Content");
            string[] dirs = Directory.GetDirectories(contentRoot);
            packsToBuild.AddRange(dirs);
            return Build(packsToBuild);
        }

        public static ContentManifest Build(IEnumerable<string> from)
        {
            ContentManifest manifest = new ContentManifest();

            foreach (var pack in from)
            {
                string packName = pack.Substring(pack.LastIndexOf('/') + 1);
                Debug.Log($"Indexing '{pack}'..");
                string root = Path.Combine(pack, pack);
                var files = Directory.GetFiles(root, "*", SearchOption.AllDirectories)
                    .Where(x => Path.GetExtension(x) != ".meta")
                    .Select(x => Path.Combine(packName, x.Substring(pack.Length + 1)));

                foreach (var file in files)
                {
                    manifest._files.Add(file);
                }
            }

            return manifest;
        }

        public static ContentManifest LoadFrom (string path)
        {
            string[] files = File.ReadAllLines(path);
            ContentManifest manifest = new ContentManifest(files);
            return manifest;
        }

        public static ContentManifest Load() => LoadFrom(PATH);

        public void Store(string path)
        {
            File.WriteAllLines(path, Files);
        }

        public void Store() => Store(PATH);

        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)_files).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_files).GetEnumerator();
        }
    }
}
