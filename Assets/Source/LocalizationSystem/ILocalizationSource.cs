using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.LocalizationSystem
{
    public interface ILocalizationSource
    {
        IEnumerable<KeyValuePair<string, string>> GetTranslations(string cultureName);
    }
}
