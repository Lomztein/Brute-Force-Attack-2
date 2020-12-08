using Lomztein.BFA2.ContentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.LocalizationSystem
{
    public class ContentLocalizationSource : ILocalizationSource
    {
        public IEnumerable<KeyValuePair<string, string>> GetTranslations(string cultureName)
        {
            var localizations = Content.GetAll<LocalizationData>("*/Localizations");

            foreach (LocalizationData data in localizations)
            {
                foreach (var pair in data)
                {
                    yield return pair;
                }
            }
        }
    }
}
