using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Stats
{
    [Serializable]
    public class StatBaseValues : IStatBaseValues
    {
        public IdentifierValuePair[] BaseValues;

        public float GetBaseValue (string identifier)
        {
            var baseValue = BaseValues.FirstOrDefault(x => x.Identifier == identifier);
            if (baseValue == null)
            {
                throw new InvalidOperationException ($"Missing base value for {nameof(identifier)}: '{identifier}'.");
            }
            return baseValue.Value;
        }

        [Serializable]
        public class IdentifierValuePair
        {
            public string Identifier;
            public float Value;
        }
    }
}
