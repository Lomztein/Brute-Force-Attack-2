using Lomztein.BFA2.Abilities;
using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Player.Interrupt;
using Lomztein.BFA2.Player.Profile;
using Lomztein.BFA2.Scenes.Battlefield;
using Lomztein.BFA2.Scenes.Battlefield.Mutators;
using Lomztein.BFA2.UI.Displays.Dialog;
using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2.Battlefield
{
    public class BattlefieldController : MonoBehaviour, InputMaster.IBattlefieldActions
    {
        public static BattlefieldController Instance { get; private set; }

        public MapController MapController;
        public RoundController RoundController;
        public InterruptIfDialogOpen StartWaveIntterupt;
        public BattlefieldSettings CurrentSettings;

        private InputMaster _master;
        public DialogTree Introduction;

        public event Action OnBattlefieldInitialized;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (BattlefieldInitializeInfo.InitType == BattlefieldInitializeInfo.InitializeType.New)
            {
                InitializeBattlefield(BattlefieldInitializeInfo.NewSettings);
                Invoke(nameof(SendStartingMessage), 2f);
            }

            if (BattlefieldInitializeInfo.InitType == BattlefieldInitializeInfo.InitializeType.Load)
            {
                InitializeBattlefield(BattlefieldSave.LoadFromFile(BattlefieldInitializeInfo.LoadFileName));
            }
            OnBattlefieldInitialized?.Invoke();
        }

        private void OnDestroy()
        {
            _master.Battlefield.Disable();
        }

        public void InitializeBattlefield (BattlefieldSettings settings)
        {
            CurrentSettings = settings;

            InitMap(settings);
            InitWaves(settings);
            InitDefaultUnlocks(settings);
            InitStartingAbilities(settings);
            InitStartingItems(settings);
            InitDifficulty(settings);
            InitMutators(settings);

            _master = new InputMaster();
            _master.Battlefield.SetCallbacks(this);
            _master.Battlefield.Enable();
        }

        public void InitializeBattlefield(BattlefieldSave save)
        {
            StartCoroutine(DelayedLoadSave(save));
        }

        private IEnumerator DelayedLoadSave(BattlefieldSave save)
        {
            CurrentSettings = save.Settings;
            yield return new WaitForEndOfFrame();
            BattlefieldSave.LoadToBattlefield(save, this);
        }

        private void InitMutators(BattlefieldSettings settings)
        {
            foreach (Mutator mutator in settings.Mutators)
            {
                mutator.Start();
            }
        }

        private void InitDifficulty(BattlefieldSettings settings)
        {
            settings.Difficulty.Apply();
        }

        private void InitStartingItems(BattlefieldSettings settings)
        {
            foreach (var item in settings.StartingItems)
            {
                Player.Player.Inventory.AddItem(Instantiate(item));
            }
        }

        private void InitStartingAbilities(BattlefieldSettings settings)
        {
            foreach (var ability in settings.StartingAbilities)
            {
                AbilityManager.Instance.AddAbility(Instantiate(ability), this);
            }
        }

        private void SendStartingMessage()
        {
            DialogDisplay.DisplayDialog(Introduction);
        }

        private void InitMap(BattlefieldSettings settings)
        {
            MapData mapData = Content.GetAll<MapData>("*/Maps/*").First(x => x.Identifier == settings.MapIdentifier);
            MapController.ApplyMapData(mapData.DeepClone());
        }

        private void InitWaves(BattlefieldSettings settings)
        {
            WaveCollection waves = Content.GetAll<WaveCollection>("*/WaveCollections/*").First(x => x.Identifier == settings.WaveCollectionIdentifier);
            RoundController.SetWaveCollection(waves.DeepClone());
        }

        private void InitDefaultUnlocks(BattlefieldSettings settings)
        {
            foreach (string comp in settings.StartingUnlocks)
            {
                Player.Player.Unlocks.SetUnlocked(comp, true);
            }
        }

        public void OnStartWave(InputAction.CallbackContext context)
        {
            if (context.performed && !StartWaveIntterupt.IsInterrupted())
            {
                RoundController.BeginNextWave();
            }
        }

        public void OnToggleFastMode(InputAction.CallbackContext context)
        {
        }
    }
}
