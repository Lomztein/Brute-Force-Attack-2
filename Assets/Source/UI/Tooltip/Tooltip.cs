using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Tooltip
{
    public class Tooltip : MonoBehaviour, ITooltip
    {
        public string _Title;
        public string _Description;
        public string _Footnote;

        public string Title => _Title;

        public string Description => _Description;

        public string Footnote => _Footnote;
    }
}
