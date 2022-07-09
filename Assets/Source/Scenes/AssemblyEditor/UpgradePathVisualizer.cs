using Lomztein.BFA2.Structures.Turrets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lomztein.BFA2.Scenes.AssemblyEditor
{
    public class UpgradePathVisualizer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private bool _holding;

        public Tier From;
        public Tier To;
        private Action<UpgradePathVisualizer> _onClick;

        public void OnPointerDown(PointerEventData eventData)
        {
            _holding = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _holding = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_holding)
            {
                _onClick(this);
            }
        }

        public void Assign(Tier from, Tier to, Action<UpgradePathVisualizer> onClick)
        {
            From = from;
            To = to;
            _onClick = onClick;
        }
    }
}
