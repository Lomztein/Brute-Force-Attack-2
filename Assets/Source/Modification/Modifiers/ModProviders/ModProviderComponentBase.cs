using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders
{
    public abstract class ModProviderComponentBase : MonoBehaviour, IModProvider
    {
        public IMod Mod { get; protected set; }

        public abstract void ApplyMod();
        public abstract void RemoveMod();

        protected virtual void Awake ()
        {
            Mod = GetComponent<IMod>();
        }

        protected void DelayedRefresh ()
        {
            this.Refresh();
        }

        private IEnumerator InternalDelayedRefresh ()
        {
            yield return new WaitForEndOfFrame();
            this.Refresh();
        }
    }
}
