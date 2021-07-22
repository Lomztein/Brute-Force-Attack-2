using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats.Formatters
{
    public class PercentageFormatter : IStatFormatter
    {
        public string Format(float value)
            => $"{Mathf.Round(value * 100)}%";
    }
}
