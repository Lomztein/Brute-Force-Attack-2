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
        public Transform TextTransform;
        public Text TitleText;

        public GameObject EnemyTypeDisplayPrefab;
        public Transform EnemyTypeDisplayParent;
        public int Wave;

        public void Display(int wave, WaveHandler handler, WaveTimeline timeline)
        {
            foreach (Transform child in transform)
            {
                if (child != TextTransform)
                {
                    Destroy(child.gameObject);
                }
            }

            Wave = wave;

            TitleText.text = wave == -1 ? "Next wave" : $"Wave {wave}";
            var dict = timeline.GetEnemySpawnAmount();
            foreach (var pair in dict)
            {
                EnemyTypeDisplay display = Instantiate(EnemyTypeDisplayPrefab, EnemyTypeDisplayParent).GetComponent<EnemyTypeDisplay>();
                display.Display(handler, pair.Value, pair.Key);
            }
        }
    }
}
