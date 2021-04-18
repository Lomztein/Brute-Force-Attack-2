using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Structures.Turrets.Connectors
{
    public class ConnectorModProvider : MonoBehaviour
    {
        [SerializeField, ModelProperty, SerializeReference, SR]
        private IMod _mod;
        [ModelProperty] [SerializeField]
        private string[] _behavioursToDisable;

        public IMod Mod => _mod; // Why? Because there are at least some of you who'd ree if I didn't. I'd probably ree too tbh.

        private void SetBehaviourEnabled(bool state)
        {
            foreach (string behaviour in _behavioursToDisable)
            {
                (GetComponent(behaviour) as Behaviour).enabled = state;
            }
        }

        public void EnableComponents()
            => SetBehaviourEnabled(true);

        public void DisableComponents()
            => SetBehaviourEnabled(false);
    }
}
