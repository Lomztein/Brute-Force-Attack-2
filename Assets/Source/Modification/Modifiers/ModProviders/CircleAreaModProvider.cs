using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Rangers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders
{
    public class CircleAreaModProvider : ModProviderComponentBase, IGlobalUpdateReciever<ModdableAddedMessage>, IRanger
    {
        [ModelProperty]
        public LayerMask TargetLayer;
        [ModelProperty]
        public float Range;
        [ModelProperty]
        public bool IncludeSelf;

        private List<IModdable> _lastModified = new List<IModdable>();

        private void Start()
        {
            ApplyMod();
        }

        private void OnDestroy()
        {
            RemoveMod();
        }

        public override void ApplyMod ()
        {
            RemoveMod();
            foreach (var moddable in GetModdables())
            {
                if (moddable.IsCompatableWith(Mod))
                {
                    _lastModified.Add(moddable);
                    moddable.Mods.AddMod(Mod);
                }
            }
        }

        public void OnGlobalUpdateRecieved(ModdableAddedMessage message)
        {
            DelayedRefresh();
        }

        public override void RemoveMod()
        {
            foreach (var moddable in _lastModified)
            {
                moddable.Mods.RemoveMod(Mod.Identifier);
            }
            _lastModified.Clear();
        }

        private IEnumerable<IModdable> GetModdables()
            => Physics2D.OverlapCircleAll(transform.position, Range, TargetLayer).Select(x => x.GetComponent<IModdable>()).Where(x => x != null && IncludeSelf ? true : x != GetComponent<IModdable>());

        public float GetRange() => Range;
    }
}
