using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Testing
{
    class OnClickModProvider : MonoBehaviour
    {
        private IMod _mod;
        public GameObject TestObject;

        private void Awake()
        {
            _mod = GetComponent<IMod>();
        }

        public void Apply()
        {
            ApplyTo(TestObject);
        }

        public void Remove()
        {
            RemoveFrom(TestObject);
        }

        private void ApplyTo (GameObject go)
        {
            TestObject.GetComponent<IModdable>()?.Mods?.AddMod(_mod);
        }

        private void RemoveFrom (GameObject go)
        {
            TestObject.GetComponent<IModdable>()?.Mods?.RemoveMod(_mod);
        }
    }
}
