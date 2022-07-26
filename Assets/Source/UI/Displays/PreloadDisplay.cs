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
        public RectTransform DisplayParent;

        public Text MainText;

        public Slider StepBar;
        public Slider PieceBar;

        public Text StepText;
        public Text PieceText;

        private void LateUpdate()
        {
            DisplayParent.gameObject.SetActive(!Preloader.Done);
            if (!Preloader.Done)
            {
                StepBar.value = (float)Preloader.CurrentStep / Preloader.TotalSteps;
                PieceBar.value = (float)Preloader.CurrentPiece / Preloader.TotalPieces;

                StepText.text = $"{Preloader.CurrentStep} / {Preloader.TotalSteps}";
                PieceText.text = $"{Preloader.CurrentPiece} / {Preloader.TotalPieces}";

                MainText.text = $"Preloading..\n{Preloader.StepName.Split(' ')[0]}\n{Preloader.PieceName}";
            }
        }
    }
}
