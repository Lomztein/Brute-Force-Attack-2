using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2
{
    [Serializable]
    public class TagSet : ITagged, IEnumerable<string>
    {
        [SerializeField]
        private List<string> _tags = new List<string>();

        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)_tags).GetEnumerator();
        }

        public bool AddTag (string tag)
        {
            if (HasTag(tag))
            {
                return false;
            }
            _tags.Add(tag);
            return true;
        }

        public bool RemoveTag (string tag)
        {
            if (HasTag(tag))
            {
                _tags.Remove(tag);
                return true;
            }
            return false;
        }

        public bool HasTag(string tag)
        {
            return _tags.Contains(tag);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)_tags).GetEnumerator();
        }
    }
}
