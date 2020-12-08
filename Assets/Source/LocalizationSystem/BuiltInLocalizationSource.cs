using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.LocalizationSystem
{
    public class BuiltInLocalizationSource : ILocalizationSource
    {
        private static readonly string _path = Path.Combine(Application.streamingAssetsPath, "Localizations");

        public IEnumerable<KeyValuePair<string, string>> GetTranslations(string cultureName)
        {
            string file = Path.Combine(_path, cultureName) + ".json";
            if (File.Exists(file))
            {
                JToken token = JToken.Parse(File.ReadAllText(file));
                LocalizationData data = ObjectPipeline.BuildObject<LocalizationData>(token);

                foreach (var pair in data)
                {
                    yield return pair;
                }
            }
        }
    }
}
