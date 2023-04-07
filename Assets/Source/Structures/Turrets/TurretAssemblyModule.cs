using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures
{
    public class TurretAssemblyModule : MonoBehaviour
    {
        public const string PREFAB_PATH = "Prefabs/ModulePrefab";

        public ModuleItem Item;

        public void AttachTo(Structure structure)
        {
            // TODO: maybe move stuff in here idk
        }

        public static TurretAssemblyModule CreateFor(ModuleItem item)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>(PREFAB_PATH));
            RootModBroadcaster broadcaster = obj.GetComponent<RootModBroadcaster>();
            TurretAssemblyModule module = broadcaster.GetComponent<TurretAssemblyModule>();

            Mod mod = item.Mod;
            module.Item = item;
            broadcaster.Mod = mod;
            broadcaster.ModCoeffecient = item.Coeffecient;

            return module;
        }
    }
}
