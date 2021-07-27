using Lomztein.BFA2.ContentSystem.References;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Loot
{
    public class RollResult : IEnumerable<KeyValuePair<ContentPrefabReference, int>>
    {
        private Dictionary<ContentPrefabReference, int> _dict = new Dictionary<ContentPrefabReference, int>();

        public void Add(ContentPrefabReference prefab, int amount) => _dict.Add(prefab, amount);

        public IEnumerator<KeyValuePair<ContentPrefabReference, int>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<ContentPrefabReference, int>>)_dict).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<ContentPrefabReference, int>>)_dict).GetEnumerator();
        }
    }
}
