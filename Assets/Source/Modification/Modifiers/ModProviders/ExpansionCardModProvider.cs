using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders
{
    public class ExpansionCardModProvider : MonoBehaviour
    {
        private IMod _mod;

        private void Awake()
        {
            _mod = GetComponent<IMod>();
        }

        public void ApplyTo (GameObject obj)
        {
            obj.GetComponent<IModdable>()?.Mods?.AddMod(_mod);
        }

        public void RemoveFrom (GameObject obj)
        {
            obj.GetComponent<IModdable>()?.Mods?.RemoveMod(_mod);
        }
    }
}
