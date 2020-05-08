using System;
using System.Collections.Generic;
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

        public ContentPack(string path, string name, string author, string description)
        {
            Name = name;
            Path = path;
            Author = author;
            Description = description;
        }

        public object GetContent(string path, Type type)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
