using Lomztein.BFA2.ContentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2
{
    public class PreloadDisplay : MonoBehaviour
    {
        public Preloader Preloader;

        public Slider ProgressBar;
        public Text CurrentText;

        private void LateUpdate()
        {
            ProgressBar.value = Preloader.PreloadProgress;
            if (!Preloader.Done)
            {
                CurrentText.text = "Preloading: " + Preloader.CurrentPreload.Split()[0];
            }
            else
            {
                CurrentText.text = "Preloading complete!";
            }

            if (Preloader.Done && !IsInvoking())
            {
                Invoke(nameof(Hide), 3f);
            }
        }

        private void Hide ()
        {
            gameObject.SetActive(false);
        }
    }
}
