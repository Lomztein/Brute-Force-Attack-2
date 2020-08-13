using Lomztein.BFA2.Enemies.Waves;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays.UpcomingEnemies
{
    public class UpcomingDisplay : MonoBehaviour
    {
        public Text TitleText;
        public Transform UpcomingParent;

        public GameObject[] WaveDisplays;

        private IWave _wave;

        public void SetTitle (string title)
        {
            TitleText.text = title;
        }

        public IWaveDisplay Display(IWave wave)
        {
            foreach (Transform child in UpcomingParent)
            {
                Destroy(child.gameObject);
            }

            _wave = wave;

            GameObject go = GetWaveDisplay(_wave);
            IWaveDisplay instantiated = Instantiate(go, UpcomingParent).GetComponent<IWaveDisplay>();
            instantiated.Display(_wave);

            (UpcomingParent as RectTransform).sizeDelta += Vector2.up; // Total hack but it works.

            return instantiated;
        }

        private GameObject GetWaveDisplay (IWave wave)
        {
            foreach (GameObject go in WaveDisplays)
            {
                IWaveDisplay display = go.GetComponent<IWaveDisplay>();
                if (display.CanDisplay(wave))
                {
                    return go;
                }
            }

            return null;
        }
    }
}
