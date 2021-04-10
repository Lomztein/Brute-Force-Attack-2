using System;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    // TODO: Implement variable pre-loading.
    public interface IContentPack
    {
        string Name { get; }
        string Author { get; }
        string Description { get; }
        string Version { get; }
        Texture2D Image { get; }

        bool RequireReload { get; }

        void Init();

        object GetContent(string path, Type type);
        object[] GetAllContent(string path, Type type);
    }
}