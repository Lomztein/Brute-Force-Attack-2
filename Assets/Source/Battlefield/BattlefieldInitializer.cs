using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Player.Messages;
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
        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        private static string DefaultMapPath = "Core/Maps/Classic.json";
        private static string DefaultWaveCollection = "Core/WaveCollections/DefaultGenerator.json";

        private void Start ()
        {
            InitMap();
            InitWaves();
            InitDefaultUnlocks();
            InitStartingItems();
            InitDifficulty();

            Invoke(nameof(SendStartingMessage), 2f);
        }

        private void InitDifficulty()
        {
            BattlefieldSettings.Difficulty.Apply();
        }

        private void InitStartingItems()
        {
        }

        private void SendStartingMessage ()
        {
            Message.Send("Defend the green shit.", 5, Message.Type.Major);
        }

        private void InitMap ()
        {
            MapData mapData = ContentSystem.Content.GetAll<MapData>("*/Maps/").First(x => x.Identifier == BattlefieldSettings.MapIdentifier);
            _mapController.IfExists((controller) => controller.ApplyMapData(mapData.DeepClone()));
        }

        private void InitWaves ()
        {
            IWaveCollection waves = ContentSystem.Content.GetAll<IWaveCollection>("*/WaveCollections").First(x => x.Identifier == BattlefieldSettings.WaveCollectionIdentifier);
            _roundController.IfExists(x => x.SetWaveCollection(waves.DeepClone()));
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
