using System;

namespace Lomztein.BFA2.Content
{
    public interface IContentManager
    {
        IContentPack[] GetContentPacks();

        object[] GetAllContent(string path, Type type);
        object GetContent(string path, Type type);
        void Init();
    }
}