using Lomztein.BFA2.Battlefield;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield
{
    public static class BattlefieldInitializeInfo
    {
        public enum InitializeType { New, Load }
        public static InitializeType InitType = InitializeType.New;

        public static BattlefieldSettings NewSettings;
        public static string LoadFileName;
    }
}
