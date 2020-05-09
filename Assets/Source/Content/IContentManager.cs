using System;

namespace Lomztein.BFA2.Content
{
    public interface IContentManager
    {
        object[] GetAllContent(string path, Type type);
        object GetContent(string path, Type type);
        void Init();
    }
}