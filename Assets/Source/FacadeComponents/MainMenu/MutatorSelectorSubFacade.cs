using Lomztein.BFA2.Scenes.Battlefield.Mutators;
using Lomztein.BFA2.Scenes.MainMenu;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.FacadeComponents.MainMenu
{
    public class MutatorSelectorSubFacade : SceneFacadeSubComponent<MainMenuFacade>
    {
        private List<Mutator> _addedMutators = new List<Mutator>();

        public override void OnSceneLoaded()
        {
            MutatorSelector selector = GetSelector();
            foreach (Mutator mutator in _addedMutators)
            {
                selector.AddMutator(mutator.DeepClone());
            }
        }

        internal void RemoveMutator(Mutator mutator)
        {
            _addedMutators.RemoveAll(x => x.Identifier == mutator.Identifier);
            if (Active)
            {
                GetSelector().RemoveMutator(mutator.Identifier);
            }
        }

        public override void OnSceneUnloaded()
        {
        }

        public void AddMutator (Mutator mutator)
        {
            _addedMutators.Add(mutator);
            if (Active)
            {
                GetSelector().AddMutator(mutator.DeepClone());
            }
        }

        private MutatorSelector GetSelector() => GameObject.Find("Mutators").GetComponent<MutatorSelector>();
    }
}
