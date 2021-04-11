using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.LocalizationSystem
{
    public static class Localization
    {
        private static LocalizationData _current;
        private static IEnumerable<ILocalizationSource> _localizationSources;

        private static IEnumerable<ILocalizationSource> GetSources ()
        {
            if (_localizationSources == null)
            {
                _localizationSources = ReflectionUtils.InstantiateAllOfType<ILocalizationSource>();
            }
            return _localizationSources;
        }

        public static void LoadLocalizations (string cultureName)
        {
            LocalizationData newData = new LocalizationData(CultureInfo.GetCultureInfo(cultureName));

            foreach (var source in GetSources())
            {
                foreach (var pair in source.GetTranslations(cultureName))
                {
                    newData.Add(pair.Key, pair.Value);
                }
            }

            _current = newData;
        }

        public static string Get(string key, params object[] values) => _current != null ? _current.Get(key, values) : key;
        public static CultureInfo GetCurrentCulture() => _current.Culture;
    }
}
