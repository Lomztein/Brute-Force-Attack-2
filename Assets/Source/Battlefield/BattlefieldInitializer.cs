using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Battlefield
{
    public class BattlefieldInitializer : MonoBehaviour
    {
        private LooseDependancy<MapController> _mapController = new LooseDependancy<MapController>();
        private static string DefaultMapPath = "Core/Maps/Classic.json";

        private void Start ()
        {
            InitMap();
            InitDefaultUnlocks();
            InitStartingItems();
            InitDifficulty();
        }

        private void InitDifficulty()
        {
            BattlefieldSettings.Difficulty.Apply();
        }

        private void InitStartingItems()
        {
        }

        private void InitMap ()
        {
            MapData mapData = BattlefieldSettings.Map ?? Content.Content.Get(DefaultMapPath, typeof(MapData)) as MapData;
            _mapController.IfExists((controller) => controller.ApplyMapData(mapData));
        }

        private void InitDefaultUnlocks ()
        {
            foreach (string comp in BattlefieldSettings.DefaultUnlockedComponents)
            {
                UnlockLists.Get("Components").SetUnlocked(comp, true);
            }
            foreach (string structure in BattlefieldSettings.DefaultUnlockedStructures)
            {
                UnlockLists.Get("Structures").SetUnlocked(structure, true);
            }
        }
    }
}
