using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.UI.Displays.Dialog.OptionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Dialog
{
    public class WaveIntroductionController : MonoBehaviour
    {
        private IEnumerable<WaveIntroduction> _introductions;

        private void Start ()
        {
            _introductions = Content.GetAll<WaveIntroduction>("*/Dialog/WaveIntroduction");
            RoundController.Instance.OnWaveStarted += Instance_OnNextWaveChanged;
        }

        private void Instance_OnNextWaveChanged(int wave, WaveHandler waveHandler)
        {
            DialogDisplay.EndDialog();
            var nodes = _introductions.Where(x => x.ShouldShow(wave + 1)).Select(x => x.GenerateDialogNode()).ToArray();
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                var result = new NextNodeDialogOptionResult();
                result.Next = nodes[i + 1];
                nodes[i].Options[0].Result = result;
            }
            if (nodes.Length > 0)
            {
                DialogDisplay.DisplayDialogNode(nodes[0]);
            }
        }

        private void OnDestroy ()
        {
            RoundController.Instance.OnWaveStarted -= Instance_OnNextWaveChanged;
        }
    }
}
