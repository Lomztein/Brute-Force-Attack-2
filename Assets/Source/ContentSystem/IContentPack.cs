using System;
using System.Collections;
using System.Collections.Generic;
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

        void Init();

        object LoadContent(string path, Type asType, IEnumerable<string> patches);

        IEnumerable<string> GetContentPaths();
        IEnumerable<ContentOverride> GetContentOverrides();
        IEnumerable<ContentPatch> GetContentPatches();
    }
}