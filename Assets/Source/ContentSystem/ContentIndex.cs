using DotNet.Globbing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentIndex
    {
        public const string OVERRIDES_FOLDER = "Override";
        public const string PATCH_FOLDER = "Patch";

        private List<string> _index = new List<string>();
        private Dictionary<string, string> _overrides = new Dictionary<string, string>();
        private Dictionary<string, List<string>> _patches = new Dictionary<string, List<string>>();

        internal void AddIndices (IEnumerable<string> indices)
        {
            foreach (var index in indices)
            {
                AddIndex(index);
            }
        }

        internal void AddIndex (string path)
        {
            _index.Add(path);
        }

        internal void AddOverride(string toOverridePath, string overridePath)
        {
            if (!_overrides.ContainsKey(toOverridePath))
            {
                _overrides.Add(toOverridePath, overridePath);
            }
            else
            {
                _overrides[toOverridePath] = overridePath;
            }
        }

        internal void AddPatch(string toPatchPath, string patchPath)
        {
            if (!_patches.ContainsKey(toPatchPath))
            {
                _patches.Add(toPatchPath, new List<string>());
                _patches[toPatchPath].Add(patchPath);
            }
            _patches[toPatchPath].Add(patchPath);
        }

        internal bool TryGetOverride(string path, out string overridePath)
            => _overrides.TryGetValue(path, out overridePath);

        internal IEnumerable<string> GetPatches(string path)
        {
            if (_patches.TryGetValue(path, out List<string> patchPaths))
            {
                return patchPaths;
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        internal IEnumerable<string> Query(string pattern)
        {
            return Query(_index, pattern);
        }

        public static IEnumerable<string> Query(IEnumerable<string> against, string pattern)
        {
            Glob glob = Glob.Parse(pattern);
            return against.Where(x => glob.IsMatch(x));
        }

        internal void ClearIndex()
        {
            _index.Clear();
            _overrides.Clear();
            _patches.Clear();
        }

    }
}
