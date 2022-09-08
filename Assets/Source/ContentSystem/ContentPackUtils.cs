using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lomztein.BFA2.ContentSystem
{
    public static class ContentPackUtils
    {
        private const char PART_SEPERATOR = '-';

        public static string GetUniqueIdentifier(this IContentPack pack, bool includeVersion = false)
        {
            string identifier = pack.Author + PART_SEPERATOR + pack.Name;
            if (includeVersion) identifier += PART_SEPERATOR + pack.Version;
            return identifier;
        }

        public static bool MatchesUniqueIdentifier(string packIdentifier, string compareIdentifier, bool includeVersion)
        {
            var packSplit = packIdentifier.Split(PART_SEPERATOR);
            var compareSplit = compareIdentifier.Split(PART_SEPERATOR);
            int compareLength;

            if (includeVersion)
            {
                Assert.IsTrue(packSplit.Length >= 3, "Pack identifier must include author, name, and version.");
                Assert.IsTrue(compareSplit.Length >= 3, "Compare identifier must include author, name, and version.");
                compareLength = 3;
            }
            else
            {
                Assert.IsTrue(packSplit.Length >= 2, "Pack identifier must include at least author and name.");
                Assert.IsTrue(compareSplit.Length >= 2, "Compare identifier must include at least author and name.");
                compareLength = 2;
            }

            bool all = true;
            for (int i = 0; i < compareLength; i++)
            {
                if (!packSplit[i].Equals(compareSplit[i]))
                {
                    all = false;
                }
            }

            return all;
        }

        public const string OVERRIDES_SUBFOLDER = "Override";
        public static IEnumerable<ContentOverride> GetOverridesFromContentPaths(this IContentPack pack)
        {
            var content = pack.GetContentPaths().ToArray();
            var overrides = pack.GetContentPaths().Where(x => x.StartsWith(OVERRIDES_SUBFOLDER));
            foreach (var over in overrides)
            {
                string sub = over.Substring(OVERRIDES_SUBFOLDER.Length + 1);
                string overridePack = sub.Substring(0, sub.IndexOf('\\'));
                string localPath = sub.Substring(sub.IndexOf('\\'));
                yield return new ContentOverride(overridePack + localPath, over);
            }
        }

        public const string PATCH_SUBFOLDER = "Patch";
        public static IEnumerable<ContentPatch> GetPatchesFromContentPaths(this IContentPack pack)
        {
            var patches = pack.GetContentPaths().Where(x => x.StartsWith(PATCH_SUBFOLDER));
            foreach (var patch in patches)
            {
                string sub = patch.Substring(PATCH_SUBFOLDER.Length + 1);
                string patchPack = sub.Substring(0, sub.IndexOf('/'));
                string localPath = sub.Substring(sub.IndexOf('/'));
                yield return new ContentPatch(patchPack + localPath, new[] { patch });
            }
        }
    }
}
