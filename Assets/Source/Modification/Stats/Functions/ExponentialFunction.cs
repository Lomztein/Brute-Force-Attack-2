using Lomztein.BFA2.Modification.Stats.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats.Functions
{
    public class ExponentialFunction : IStatFunction
    {
        public float ComputeStat(float baseValue, float value)
            => Mathf.Pow(baseValue, value);

        public string FormatEquation(float baseValue, float value)
            => $"{baseValue}^{value}";
    }
}
