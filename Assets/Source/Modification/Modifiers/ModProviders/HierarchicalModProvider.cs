using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders
{
    public class HierarchicalModProvider : ModProviderComponentBase, IGlobalUpdateReciever<ModdableAddedMessage>
    {
        [ModelProperty]
        public bool ProvideUpwards;
        [ModelProperty]
        public bool ProvideDownwards;
        [ModelProperty]
        public bool ProvideSelf;

        private readonly List<IModdable> _lastModified = new List<IModdable>();

        private void Start()
        {
            DelayedRefresh();
        }

        private void OnDestroy()
        {
            RemoveMod();
        }

        public override void ApplyMod ()
        {
            foreach (IModdable moddable in GetModdables())
            {
                if (moddable.IsCompatableWith(Mod))
                {
                    _lastModified.Add(moddable);
                    moddable.Mods.AddMod(Mod);
                }
            }
        }

        public override void RemoveMod()
        {
            UnmodLastModifed();
        }

        public void OnGlobalUpdateRecieved(ModdableAddedMessage message)
        {
            DelayedRefresh();
        }

        private void UnmodLastModifed ()
        {
            foreach (var moddable in _lastModified)
            {
                if (moddable != null)
                {
                    moddable.Mods.RemoveMod(Mod);
                }
            }
            _lastModified.Clear();
        }

        private IEnumerable<IModdable> GetModdables()
        {
            List<IModdable> moddables = new List<IModdable>();
            IModdable self = GetComponent<IModdable>();

            if (ProvideUpwards)
            {
                IEnumerable<IModdable> upwards = GetComponentsInParent<IModdable>().Where(x => x != self);
                moddables.AddRange(upwards);
            }
            if (ProvideDownwards)
            {
                IEnumerable<IModdable> downwards = GetComponentsInChildren<IModdable>().Where(x => x != self);
                moddables.AddRange(downwards);
            }
            if (ProvideSelf)
            {
                moddables.Add(self);
            }

            return moddables;
        }
    }
}
