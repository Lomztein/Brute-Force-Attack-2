using Lomztein.BFA2.Modification.Stats.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Stats.Functions
{
    public class LiniarFunction : IStatFunction
    {
        public float ComputeStat(float baseValue, float value)
            => baseValue + value;

        public string FormatEquation(float baseValue, float value)
            => $"{baseValue} + {value}";
    }
}
