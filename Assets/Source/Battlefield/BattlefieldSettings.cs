using Lomztein.BFA2.Battlefield.Difficulty;
using Lomztein.BFA2.Enemies.Scalers;
using Lomztein.BFA2.Inventory.Items;
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
        public static MapData Map;
        public static DifficultySettings Difficulty = new DifficultySettings();

        public static string[] DefaultUnlockedComponents =
        {
            "Core.SmallBase",
            "Core.SmallRotator",
            "Core.LaserCannonT1",
            "Core.MachineGunT1",
            "Core.RocketLauncherT1",
            "Core.AuxilleryProcessor"
        };
        public static string[] DefaultUnlockedStructures =
        {
            "Core.Collector",
            "Core.Lubricator"
        };
    }
}
