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
        private const string IGNORE_PREFIX = "IGNORE_"; // Any files prefixed with this will be ignored.
        private const string JSON_FILE_EXTENSION = ".json"; // Any files prefixed with this will be ignored.
        private readonly string _path;

        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }

        private IContentLoader _contentLoader = new ContentLoader();

        public ContentPack(string path, string name, string author, string description)
        {
            _path = path;
            Name = name;
            Author = author;
            Description = description;
        }

        public object GetContent(string path, Type type)
        {
            return _contentLoader.LoadContent(OSAgnosticPath(_path + path), type);
        }

        private string OSAgnosticPath (string path)
        {
            return 
                path.Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);
        }

        public object[] GetAllContent(string path, Type type)
        {
            List<object> content = new List<object>();
            string spath = OSAgnosticPath (_path + path);

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
    }
}
