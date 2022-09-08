using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentOverride : MonoBehaviour
    {
        public string OriginalPath { get; private set; }
        public string OverridePath { get; private set; }

        public ContentOverride(string originalPath, string overridePath)
        {
            OriginalPath = originalPath;
            OverridePath = overridePath;
        }
    }
}
