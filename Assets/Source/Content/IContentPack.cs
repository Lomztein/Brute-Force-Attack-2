using System;

namespace Lomztein.BFA2.Content
{
    public interface IContentPack
    {
        string Name { get; }
        string Author { get; }
        string Description { get; }

        object GetContent(string path, Type type);

        object[] GetAllContent(string path, Type type);
    }
}