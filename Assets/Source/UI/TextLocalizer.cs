using Lomztein.BFA2.LocalizationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI
{
    public class TextLocalizer : MonoBehaviour
    {
        public string Key;

        private void Awake()
        {
            Text text = GetComponent<Text>();
            text.text = Localization.Get(string.IsNullOrEmpty(Key) ? text.text : Key) ;
        }
    }
}
