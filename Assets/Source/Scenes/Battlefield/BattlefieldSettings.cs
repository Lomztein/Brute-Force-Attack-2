﻿using Lomztein.BFA2.Enemies.Scalers;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Scenes.Battlefield.Difficulty;
using Lomztein.BFA2.Scenes.Battlefield.Mutators;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Battlefield
{
    public class BattlefieldSettings
    {
        public static BattlefieldSettings CurrentSettings = new BattlefieldSettings();

        public string MapIdentifier = "Core.SnakinAbout";
        public string WaveCollectionIdentifier = "Core.DefaultGenerator";
        public Difficulty Difficulty = ScriptableObject.CreateInstance<Difficulty>();
        private List<Mutator> _mutatorsList = new List<Mutator>();
        public Mutator[] Mutators => _mutatorsList.ToArray();

        public void AddMutator(Mutator mutator) => _mutatorsList.Add(mutator);
        public void RemoveMutator(Mutator mutator) => _mutatorsList.RemoveAll (x => x.Identifier == mutator.Identifier);

        public string[] DefaultUnlockedComponents =
        {
            "Core.SmallBase",
            "Core.SmallRotator",
            "Core.PulseLaserT1",
            "Core.MachineGunT1",
            "Core.RocketLauncherT1",
            "Core.AuxProcessor",
            "Core.OneToTwoConnector",
            "Core.SmallQuadFireSynchronizer",
            "Core.SmallTwinFireSynchronizer"
        };
        public string[] DefaultUnlockedStructures =
        {
            "Core.Collector",
            "Core.Lubricator"
        };
    }
}
