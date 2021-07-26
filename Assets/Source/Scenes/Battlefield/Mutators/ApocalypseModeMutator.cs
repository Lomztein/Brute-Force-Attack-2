using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public class ApocalypseModeMutator : Mutator
    {
        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        public override void Start()
        {
            _roundController.IfExists(x =>
            {
                x.OnWaveEnemiesSpawned += OnWaveFinished;
            });
        }

        private void OnWaveFinished(int arg1, WaveHandler arg2)
        {
            RoundController controller = RoundController.Instance;
            if (controller.ActiveWaves.Length < 2)
            {
                controller.BeginNextWave();
            }
        }
    }
}
