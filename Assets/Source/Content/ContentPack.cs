using Lomztein.BFA2.Content.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content
{
    public class ContentPack : IContentPack
    {
        public string Path { get; private set; }

        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }

        private IRawContentLoader _contentLoader = new RawContentLoader();

        public ContentPack(string path, string name, string author, string description)
        {
            Name = name;
            Path = path;
            Author = author;
            Description = description;
        }

        public object GetContent(string path, Type type)
        {
            return _contentLoader.LoadContent(Path + path, type);
        }

        public object[] GetAllContent(string path, Type type)
        {
            List<object> content = new List<object>();
            string spath = Path + path;

            if (Directory.Exists(spath))
            {
                string[] files = Directory.GetFiles(spath, "*.json");
                foreach (string file in files)
                {
                    content.Add(_contentLoader.LoadContent(file, type));
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
