using Lomztein.BFA2.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;

        public Canvas MainCanvas;
        public GraphicRaycaster GraphicRaycaster;
        public EventSystem EventSystem;

        public WindowManager Windows;

        private void Awake()
        {
            Instance = this;
        }
    }
}