using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.LocalizationSystem
{
    // TODO: Reimplement as composite pattern if performance is trash.
    public class LocalizationData : IAssemblable, IEnumerable<KeyValuePair<string, string>>
    {
        public CultureInfo Culture { get; private set; } = CultureInfo.GetCultureInfo("en-US");
        private Dictionary<string, string> _translations = new Dictionary<string, string>();

        public LocalizationData() { }

        public LocalizationData (CultureInfo culture)
        {
            Culture = culture;
        }

        public void Add (string key, string translation)
        {
            _translations.Add(key, translation);
        }

        public void Add(Dictionary<string, string> translations)
        {
            foreach (var pair in translations)
            {
                _translations.Add(pair.Key, pair.Value);
            }
        }

        public string Get(string key, params object[] values)
        {
            if (_translations.TryGetValue(key, out string translation))
            {
                for (int i = 0; i < values.Length; i++)
                {
                    translation = translation.Replace($"{{{i}}}", values[i].ToString());
                }
                return translation;
            }
            else
            {
                return key;
            }
        }

        public ValueModel Disassemble()
        {
            return new ObjectModel(_translations.Select(x => new ObjectField(x.Key, new PrimitiveModel(x.Value))).ToArray());
        }

        public void Assemble(ValueModel source)
        {
            ObjectModel obj = source as ObjectModel;
            _translations = obj.GetProperties().ToDictionary(x => x.Name, y => (y.Model as PrimitiveModel).Value);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, string>>)_translations).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_translations).GetEnumerator();
        }
    }
}
