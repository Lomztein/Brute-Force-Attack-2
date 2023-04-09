using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Windows
{
    public class DarkOverlay : MonoBehaviour
    {
        private static readonly float _deltaTime = 0.02f;
        public static DarkOverlay Instance;

        public Image Image;
        public RectTransform Cutout;
        public float FadeTime;

        public float MinAlpha = 0f;
        public float MaxAlpha = 0.75f;

        private Coroutine _coroutine;

        private void Awake()
        {
            Instance = this;
            ResetCutout();
        }

        public static void FadeIn ()
        {
            Instance.Fade(Instance.MaxAlpha);
        }

        public static void FadeOut ()
        {
            Instance.Fade(Instance.MinAlpha);
        }

        private void Fade (float targetAlpha)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(FadeCoroutine(targetAlpha));
        }

        private IEnumerator FadeCoroutine (float targetAlpha)
        {
            float fadeSpeed = 1f / FadeTime * Time.fixedDeltaTime;
            float original = Image.color.a;
            float delta = Mathf.Abs(original - targetAlpha);
            int fadeIters = Mathf.RoundToInt(delta / fadeSpeed);

            for (int i = 0; i < fadeIters; i++)
            {
                Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Mathf.Lerp(original, targetAlpha, (float)i / fadeIters));
                yield return new WaitForSecondsRealtime(_deltaTime);
            }
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, targetAlpha);
        }

        public static void SetCutout (Vector2 position, float size)
        {
            Instance.Cutout.position = position;
            Instance.Cutout.sizeDelta = Vector2.one * size;
            Instance.ResetDarkness();
        }

        public static void ResetCutout ()
        {
            Instance.Cutout.position = Vector3.right * 1000000;
            Instance.ResetDarkness();
        }

        private void ResetDarkness ()
        {
            Image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
            Image.rectTransform.position = Image.rectTransform.sizeDelta / 2f;
            (transform as RectTransform).ForceUpdateRectTransforms();
        }
    }
}
