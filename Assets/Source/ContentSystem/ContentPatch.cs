using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentPatch : MonoBehaviour
    {
        public string OriginalPath { get; private set; }
        public IEnumerable<string> PatchPaths { get; private set; }

        public ContentPatch(string originalPath, IEnumerable<string> patchPaths)
        {
            OriginalPath = originalPath;
            PatchPaths = patchPaths;
        }
    }
}
