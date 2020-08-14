using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders
{
    public class ConnectorModProvider : ModProviderComponentBase
    {
        [ModelProperty]
        public string[] BehavioursToDisable;

        public void DisableComponents ()
        {
            foreach (var behaviour in GetToDisable())
            {
                behaviour.enabled = false;
            }
        }

        public void EnableComponents ()
        {
            foreach (var behaviour in GetToDisable())
            {
                behaviour.enabled = true;
            }
        }

        public override void RemoveMod()
        {
            Debug.LogWarning("ConnectorModProvider does not apply on it's own, instead provide mods for TurretConnector components.");
        }

        public override void ApplyMod()
        {
            Debug.LogWarning("ConnectorModProvider does not apply on it's own, instead provide mods for TurretConnector components.");
        }

        private IEnumerable<Behaviour> GetToDisable()
            => BehavioursToDisable.Select(x => GetComponent(x)).Cast<Behaviour>();
    }
}
