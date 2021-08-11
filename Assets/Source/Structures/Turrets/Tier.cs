using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Structures.Turrets
{
    public class Tier // Should be a struct? I dunno lol
    {
        public int TierIndex { get; private set; }
        public int VariantIndex { get; private set; }


        public override bool Equals(object obj)
        {
            if (obj is Tier other)
            {
                return VariantIndex == other.VariantIndex && TierIndex == other.TierIndex;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return VariantIndex * TierIndex + VariantIndex - TierIndex;
        }

        public override string ToString()
        {
            return $"{TierIndex}-{VariantIndex}";
        }

        public bool IsTier(int tier, int variant) => tier == TierIndex && variant == VariantIndex;
    }
}
