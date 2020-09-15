using System;

namespace Lomztein.BFA2.ContentSystem
{
    public interface IContentManager
    {
        IContentPack[] GetContentPacks();

        object[] GetAllContent(string path, Type type);
        object GetContent(string path, Type type);
    }
}