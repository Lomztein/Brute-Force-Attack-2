using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lomztein.BFA2
{
    public class HoverCallback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<PointerEventData> OnEnter;
        public event Action<PointerEventData> OnExit;

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnter?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExit?.Invoke(eventData);
        }
    }
}
