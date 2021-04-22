using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.LocalizationSystem
{
    public class NoLocalizationException : Exception
    {
        public NoLocalizationException(string key, CultureInfo info) : base($"Missing localization for key '{key}' in localization for culture '{info.Name}'.")
        {
        }
    }
}
