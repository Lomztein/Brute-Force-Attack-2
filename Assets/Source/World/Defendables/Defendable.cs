using Lomztein.BFA2.Player.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.World.Defendables
{
    public abstract class Defendable : MonoBehaviour
    {
        private IHealthContainer _healthContainer;

        public void OnMapObjectAssembled ()
        {
            _healthContainer = GetComponent<IHealthContainer>();

            _healthContainer.OnHealthChanged += OnHealthChanged;
            _healthContainer.OnHealthExhausted += OnHealthExhausted;
        }

        public abstract void OnHealthChanged(float before, float after, float total, object source);

        public abstract void OnHealthExhausted(object source);
    }
}
