using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders
{
    public class SelfModProvider : ModProviderComponentBase
    {
        private IModdable _moddable;

        protected override void Awake()
        {
            base.Awake();
            _moddable = GetComponent<IModdable>();
            DelayedRefresh();
        }

        private void OnDestroy()
        {
            RemoveMod();
        }

        public override void ApplyMod ()
        {
            if (_moddable.IsCompatableWith(Mod))
            {
                _moddable.Mods.AddMod(Mod);
            }
        }

        public override void RemoveMod ()
        {
            _moddable.Mods.RemoveMod(Mod);
        }
    }
}
