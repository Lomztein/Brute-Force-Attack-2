using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Displays.Dialog
{
    public class FirstApperanceOfEnemyPredicate : IWaveIntroductionPredicate
    {
        [ModelProperty]
        public string[] EnemyIdentifiers;

        public bool ShouldShow(int forWave)
        {
            return IsFirstApperance(forWave);
        }

        private bool IsFirstApperance(int wave)
        {
            bool hasAppeared = false;
            for (int i = 0; i < wave; i++)
            {
                var timeline = RoundController.Instance.GetWave(i);
                if (timeline.Any(x => MatchesIdentifiers(x.EnemyIdentifier)))
                {
                    hasAppeared = true;
                    break;
                }
            }
            if (!hasAppeared)
            {
                return RoundController.Instance.GetWave(wave).Any(x => MatchesIdentifiers(x.EnemyIdentifier));
            }
            else return false;
        }

        private bool MatchesIdentifiers(string enemyIdentifier)
        {
            return EnemyIdentifiers.Any(x => enemyIdentifier.StartsWith(x));
        }
    }
}
