using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Mutators;
using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2.Battlefield
{
    public class BattlefieldController : MonoBehaviour, InputMaster.IBattlefieldActions
    {
        private static string DefaultMapPath = "Core/Maps/Classic.json";
        private static string DefaultWaveCollection = "Core/WaveCollections/DefaultGenerator.json";

        public MapController MapController;
        public RoundController RoundController;

        private void Start()
        {
            InitMap();
            InitWaves();
            InitDefaultUnlocks();
            InitStartingItems();
            InitDifficulty();
            InitMutators();

            Invoke(nameof(SendStartingMessage), 2f);

            InputMaster master = new InputMaster();
            master.Battlefield.SetCallbacks(this);
            master.Battlefield.Enable();
        }

        public void InitializeBattlefield (BattlefieldSettings settings)
        {
            // TODO: Refactor so battlefield initialization starts here.
        }

        private void InitMutators()
        {
            foreach (Mutator mutator in BattlefieldSettings.CurrentSettings.Mutators)
            {
                mutator.Start();
            }
        }

        private void InitDifficulty()
        {
            BattlefieldSettings.CurrentSettings.Difficulty.Apply();
        }

        private void InitStartingItems()
        {
        }

        private void SendStartingMessage()
        {
            Message.Send("Defend the green shit.", 5, Message.Type.Major);
        }

        private void InitMap()
        {
            MapData mapData = ContentSystem.Content.GetAll<MapData>("*/Maps/").First(x => x.Identifier == BattlefieldSettings.CurrentSettings.MapIdentifier);
            MapController.ApplyMapData(mapData.DeepClone());
        }

        private void InitWaves()
        {
            IWaveCollection waves = ContentSystem.Content.GetAll<IWaveCollection>("*/WaveCollections").First(x => x.Identifier == BattlefieldSettings.CurrentSettings.WaveCollectionIdentifier);
            RoundController.SetWaveCollection(waves.DeepClone());
        }

        private void InitDefaultUnlocks()
        {
            foreach (string comp in BattlefieldSettings.CurrentSettings.DefaultUnlockedComponents)
            {
                Player.Player.Unlocks.SetUnlocked(comp, true);
            }
            foreach (string structure in BattlefieldSettings.CurrentSettings.DefaultUnlockedStructures)
            {
                Player.Player.Unlocks.SetUnlocked(structure, true);
            }
        }

        public void OnStartWave(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                RoundController.BeginNextWave();
            }
        }

        public void OnToggleFastMode(InputAction.CallbackContext context)
        {
        }
    }
}
