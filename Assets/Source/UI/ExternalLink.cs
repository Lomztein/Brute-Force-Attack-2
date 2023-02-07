using Lomztein.BFA2.UI.ToolTip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI
{
    public class ExternalLink : MonoBehaviour, IHasToolTip
    {
        public string Name;
        public string Url;

        public void Click ()
        {
            Application.OpenURL(Url);
        }

        public GameObject InstantiateToolTip()
        {
            return SimpleToolTip.InstantiateToolTip(Name, null, Url);
        }
    }
}
