using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Player.Health;
using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player
{
    public class Player : MonoBehaviour, IModdable
    {
        public static Player Instance { get; private set; }
        public static event Action<Player> OnNewPlayerInstance;

        private readonly IStatContainer _stats = new StatContainer();
        private readonly IEventContainer _events = new EventContainer();

        public Resource CreditsResource;
        [ModelProperty]
        public StatInfo ResourceEarningMultiplierInfo;

        private IModContainer _mods;
        IModContainer IModdable.Mods => Instance._mods;
        public static IModContainer Mods => Instance._mods;

        private IHealthContainer _health;
        public static IHealthContainer Health => Instance._health;
        private IInventory _inventory;
        public static IInventory Inventory => Instance._inventory;

        private IResourceContainer _resources;
        public static IResourceContainer Resources => Instance._resources;

        private IUnlockList _unlocks;
        public static IUnlockList Unlocks => Instance._unlocks;

        private Dictionary<string, float> _resourceFractionTrackers;
        public Dictionary<string, IStatReference> ResourceEarningMultiplier;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _mods = new ModContainer(this, _stats, _events);
            Instance = this;

            _health = GetComponent<IHealthContainer>();
            _unlocks = GetComponent<IUnlockList>();
            _resources = GetComponent<IResourceContainer>();
            _inventory = GetComponent<IInventory>();

            if (_resources != null)
            {
                _resources?.ChangeResource(CreditsResource, 0);
            }

            ResourceEarningMultiplier = new Dictionary<string, IStatReference>();
            _resourceFractionTrackers = new Dictionary<string, float>();

            Resource[] resources = Content.GetAll<Resource>("*/Resources");
            foreach (Resource resource in resources)
            {
                ResourceEarningMultiplier.Add(resource.Identifier, _stats.AddStat(GenerateStatInfo(resource), 1f, this));
                _resourceFractionTrackers.Add(resource.Identifier, 0f);
            }


            OnNewPlayerInstance?.Invoke(this);
        }

        private StatInfo GenerateStatInfo (Resource resource)
        {
            StatInfo instance = Instantiate(ResourceEarningMultiplierInfo);
            instance.Identifier = resource.Identifier + "EarningsMultiplier";
            instance.Name = resource.Name + " Earnings Multiplier";
            return instance;
        }

        public void Earn (Resource resource, float amount)
        {
            amount *= ResourceEarningMultiplier[resource.Identifier].GetValue();

            _resourceFractionTrackers[resource.Identifier] += amount;
            int floored = Mathf.FloorToInt(_resourceFractionTrackers[resource.Identifier]);
            Resources.ChangeResource(resource, floored);
            _resourceFractionTrackers[resource.Identifier] -= floored;
        }
    }
}
