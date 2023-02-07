using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class AssignableToolTip : MonoBehaviour, IHasToolTip
    {
        private Func<GameObject> _tooltipGetter;

        public void SetToolTip (Func<GameObject> getter)
        {
            _tooltipGetter = getter;
        }

        public GameObject InstantiateToolTip()
        {
            return _tooltipGetter();
        }
    }
}
