using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Structures.Turrets
{
    [Serializable]
    public class Tier : IAssemblable
    {
        public int TierIndex;
        public int VariantIndex;
        public static Tier Initial => new Tier(0, 0);

        public Tier(int tierIndex, int variantIndex)
        {
            TierIndex = tierIndex;
            VariantIndex = variantIndex;
        }

        public Tier () { }

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
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{TierIndex.ToString()}-{VariantIndex.ToString()}";
        }

        public static Tier Parse(string input)
        {
            string[] split = input.Split('-');
            int tier = int.Parse(split[0]);
            int variant = int.Parse(split[1]);
            return new Tier(tier, variant);
        }


        public bool IsTier(int tier, int variant) => tier == TierIndex && variant == VariantIndex;

        public ValueModel Disassemble(DisassemblyContext context)
        {
            return new ObjectModel(
                new ObjectField("Tier", new PrimitiveModel(TierIndex)),
                new ObjectField("Variant", new PrimitiveModel(VariantIndex))
                );
        }

        public void Assemble(ValueModel source, AssemblyContext context)
        {
            ObjectModel obj = source as ObjectModel;
            TierIndex = obj.GetValue<int>("Tier");
            VariantIndex = obj.GetValue<int>("Variant");
        }
    }
}
