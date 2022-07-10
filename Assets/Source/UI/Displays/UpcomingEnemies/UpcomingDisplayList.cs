using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays.UpcomingEnemies
{
    public class UpcomingDisplayList : MonoBehaviour
    {
        public UpcomingDisplay NextDisplay;

        public GameObject DisplayPrefab;
        public Transform DisplayParent;
 
        private List<UpcomingDisplay> _currentDisplays = new List<UpcomingDisplay>();

        public RoundController RoundController;

        public void Start()
        {
            RoundController.OnNextWaveChanged += OnNextWaveChanged;
            RoundController.OnWaveStarted += OnWaveStarted;
            RoundController.OnWaveFinished += OnWaveFinished;
            NextDisplay.Display(-1, null, RoundController.NextWave);
        }

        private void OnDestroy()
        {
            RoundController.OnNextWaveChanged -= OnNextWaveChanged;
            RoundController.OnWaveStarted -= OnWaveStarted;
            RoundController.OnWaveFinished -= OnWaveFinished;
        }

        private void OnNextWaveChanged (int index)
        {
            NextDisplay.Display(-1, null, RoundController.NextWave);
        }

        private void OnWaveStarted(int index, WaveHandler wave)
        {
            UpcomingDisplay newDisplay;

            newDisplay = Instantiate(DisplayPrefab, DisplayParent).GetComponent<UpcomingDisplay>();
            _currentDisplays.Add(newDisplay);

            newDisplay.Display(index, wave, wave.Timeline);

            NextDisplay.Display(-1, null, RoundController.NextWave);
            NextDisplay.transform.SetAsLastSibling();
        }

        private void Organize (IEnumerable<Transform> parents, int perParent, IEnumerable<Transform> transforms, Transform excessParent)
        {
            using (IEnumerator<Transform> enumerator = transforms.GetEnumerator())
            {
                foreach (Transform parent in parents)
                {
                    for (int i = 0; i < perParent; i++)
                    {
                        if (enumerator.MoveNext())
                        {
                            enumerator.Current.SetParent(parent);
                        }
                    }
                }


                while (enumerator.MoveNext())
                {
                    enumerator.Current.SetParent(excessParent);
                }
            }
        }

        private void OnWaveFinished(int index, WaveHandler wave)
        {
            UpcomingDisplay display = _currentDisplays.First(x => x.Wave == index);
            Destroy(display.gameObject);
            _currentDisplays.Remove(display);
        }
    }
}
