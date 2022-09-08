using Lomztein.BFA2.ContentSystem.References;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Loot
{
    public class RollResult : IEnumerable<Roll>
    {
        private List<Roll> _rolls = new List<Roll>();

        public void Add(Roll roll) => _rolls.Add(roll);

        public IEnumerator<Roll> GetEnumerator()
        {
            return ((IEnumerable<Roll>)_rolls).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_rolls).GetEnumerator();
        }
    }
}
