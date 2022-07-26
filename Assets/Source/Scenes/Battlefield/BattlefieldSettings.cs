using Lomztein.BFA2.Abilities;
using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Enemies.Scalers;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Scenes.Battlefield.Difficulty;
using Lomztein.BFA2.Scenes.Battlefield.Mutators;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Battlefield
{
    [CreateAssetMenu(menuName = "BFA2/BattlefieldSettings", fileName = "New Battlefield Settings")]
    public class BattlefieldSettings : ScriptableObject
    {
        public const string DEFAULT_SETTINGS_PATH = "Resources/DefaultBattlefieldSettings";

        [ModelProperty]
        public string MapIdentifier = "Core.SnakinAbout";
        [ModelProperty]
        public string WaveCollectionIdentifier = "Core.Procedural";
        [ModelProperty]
        public Difficulty Difficulty;
        [ModelProperty, SerializeField]
        private List<Mutator> _mutatorsList = new List<Mutator>();
        public IEnumerable<Mutator> Mutators => _mutatorsList;
        [ModelProperty, SerializeField]
        private List<Item> _startingItems;
        public IEnumerable<Item> StartingItems => _startingItems;

        [ModelProperty, SerializeField]
        private List<Ability> _startingAbilities;
        public IEnumerable<Ability> StartingAbilities => _startingAbilities;

        public void AddMutator(Mutator mutator) => _mutatorsList.Add(mutator);
        public void RemoveMutator(Mutator mutator) => _mutatorsList.RemoveAll (x => x.Identifier == mutator.Identifier);

        public void AddStartingItem(Item item) => _startingItems.Add(item);
        public void RemoveStartingItem(Item item) => _startingItems.RemoveAll(x => x.Identifier == item.Identifier);

        [ModelProperty]
        public string[] StartingUnlocks =
        {
            "Core.SmallBase",
            "Core.SmallRotator",
            "Core.PulseLaserT1",
            "Core.MachineGunT1",
            "Core.RocketLauncherT1",
            "Core.OneToTwoConnector",
            "Core.SmallQuadFireSynchronizer",
            "Core.SmallTwinFireSynchronizer",
            "Core.SideMount",
            "Core.Lubricator",
            "Core.Collector"
        };

        public static BattlefieldSettings LoadDefaults() =>
            Instantiate(Content.Get<BattlefieldSettings>(DEFAULT_SETTINGS_PATH));
    }
}
