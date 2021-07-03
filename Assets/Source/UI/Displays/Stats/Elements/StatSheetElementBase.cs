using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays.Stats.Elements
{
    public abstract class StatSheetElementBase : MonoBehaviour, IStatSheetElement
    {
        public Image IconImage;
        public Text ValueText;

        public void SetIcon(Sprite sprite) => IconImage.sprite = sprite;
        public void SetText(string value) => ValueText.text = value;

        public abstract bool UpdateDisplay(GameObject target);
    }
}
