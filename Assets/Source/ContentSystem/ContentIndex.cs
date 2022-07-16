using DotNet.Globbing;
using Lomztein.BFA2.ContentSystem.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentIndex
    {
        private List<string> _index = new List<string>();

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

        internal IEnumerable<string> Query(string pattern)
        {
            Glob glob = Glob.Parse(pattern);
            return _index.Where(x => glob.IsMatch(x));
        }

        internal void ClearIndex() => _index.Clear();
    }
}
