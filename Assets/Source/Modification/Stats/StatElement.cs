using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Stats
{
    class StatElement : IStatElement
    {
        public object Owner { get; private set; }
        public float Value { get; private set; }

        public StatElement (object owner, float value)
        {
            Owner = owner;
            Value = value;
        }

        public event Action OnChanged;
    }
}
