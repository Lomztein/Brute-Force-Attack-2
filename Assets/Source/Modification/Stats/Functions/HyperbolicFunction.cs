using Lomztein.BFA2.Modification.Stats.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Stats.Functions
{
    public class HyperbolicFunction : IStatFunction
    {
        public float ComputeStat(float baseValue, float value)
            => (1 - baseValue) * 1 / (value + 1);

        public string FormatEquation(float baseValue, float value)
            => $"(1 - {baseValue}) * 1 / ({value} + 1)";
    }
}
