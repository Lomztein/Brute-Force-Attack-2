using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Player.Health;
using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Purchasing.Resources;
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

        private IModContainer _mods;
        IModContainer IModdable.Mods => Instance._mods;
        public static IModContainer Mods => Instance._mods;

        private IHealthContainer _health;
        public static IHealthContainer Health => Instance._health;

        private IResourceContainer _resources;
        public static IResourceContainer Resources => Instance._resources;

        private IUnlockList _unlocks;
        public static IUnlockList Unlocks => Instance._unlocks;

        private void Awake()
        {
            Init();
        }

        public void OnAssembled ()
        {
            Init();
        }

        private void Init()
        {
            _mods = new ModContainer(_stats, _events);
            Instance = this;

            _health = GetComponent<IHealthContainer>();
            _unlocks = GetComponent<IUnlockList>();
            _resources = GetComponent<IResourceContainer>();

            if (_health != null)
            {
                _health.ChangeHealth(0);
            }

            if (_resources != null)
            {
                _resources?.ChangeResource(Resource.Credits, 0);
            }

            OnNewPlayerInstance?.Invoke(this);
        }

        public ModdableAttribute[] GetModdableAttributes()
        {
            return Array.Empty<ModdableAttribute>();
        }
    }
}
