using Lomztein.BFA2.Battlefield.Difficulty;
using Lomztein.BFA2.Enemies.Scalers;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Mutators;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Battlefield
{
    // This needs to be an object, perhaps with a CurrentSettings static field. Must be an object that may be serialized.
    public static class BattlefieldSettings
    {
        public static string MapIdentifier = "Core.SnakinAbout";
        public static string WaveCollectionIdentifier = "Core.DefaultGenerator";
        public static DifficultySettings Difficulty = new DifficultySettings();
        private static List<Mutator> _mutatorsList = new List<Mutator>();
        public static Mutator[] Mutators => _mutatorsList.ToArray();

        public static void AddMutator(Mutator mutator) => _mutatorsList.Add(mutator);
        public static void RemoveMutator(Mutator mutator) => _mutatorsList.RemoveAll (x => x.Identifier == mutator.Identifier);

        public static string[] DefaultUnlockedComponents =
        {
            "Core.SmallBase",
            "Core.SmallRotator",
            "Core.PulseLaserT1",
            "Core.MachineGunT1",
            "Core.RocketLauncherT1",
            "Core.AuxProcessor",
            "Core.1x3Connector",
        };
        public static string[] DefaultUnlockedStructures =
        {
            "Core.Collector",
            "Core.Lubricator"
        };
    }
}
