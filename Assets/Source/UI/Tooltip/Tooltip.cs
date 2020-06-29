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
        public string _Text;

        public string Text => _Text;
    }
}
