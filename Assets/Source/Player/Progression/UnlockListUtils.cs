using Lomztein.BFA2.Research;
using Lomztein.BFA2.Research.Rewards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Player.Progression
{
    public static class UnlockListUtils
    {
        public static IEnumerable<string> GetUnlocked(this IUnlockList list, IEnumerable<string> identifiers)
            => identifiers.Where(x => list.IsUnlocked(x));

        public static IEnumerable<string> GetLocked(this IUnlockList list, IEnumerable<string> identifiers)
            => identifiers.Where(x => !list.IsUnlocked(x));

        public static IEnumerable<KeyValuePair<string, ResearchOption>> GetRequiredResearchToUnlock(this IUnlockList list, IEnumerable<ResearchOption> options, IEnumerable<string> identifiers)
        {
            foreach (string identifier in identifiers)
            {
                // If not unlocked..
                if (!list.IsUnlocked(identifier))
                {
                    // Find research that unlocks.
                    foreach (ResearchOption option in options)
                    {
                        foreach (var reward in option.Rewards)
                        {
                            if (reward is UnlockReward unlockReward && unlockReward.Unlocks.Contains(identifier))
                            {
                                yield return new KeyValuePair<string, ResearchOption>(identifier, option);
                            }
                        }
                    }
                }
            }
        }
    }
}
