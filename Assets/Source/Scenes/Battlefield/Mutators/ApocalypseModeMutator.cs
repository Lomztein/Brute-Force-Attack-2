using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Mutators
{
    public class ApocalypseModeMutator : Mutator
    {
        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        public override void Start()
        {
            _roundController.IfExists(x =>
            {
                x.OnWaveFinished += OnWaveFinished;
            });
        }

        private void OnWaveFinished(int arg1, Enemies.Waves.IWave arg2)
        {
            _roundController.IfExists(x =>
            {
                x.BeginNextWave();
            });
        }
    }
}
