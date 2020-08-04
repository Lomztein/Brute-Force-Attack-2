using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Battlefield
{
    public static class BattlefieldSettings
    {
        public static MapData Map;

        public static string[] DefaultUnlockedComponents =
        {
            "Core.SmallBase",
            "Core.SmallRotator",
            "Core.LaserCannonT1",
            "Core.MachineGunT1",
            "Core.RocketLauncherT1",
        };
        public static string[] DefaultUnlockedStructures =
        {
            "Core.Collector",
        };
    }
}
