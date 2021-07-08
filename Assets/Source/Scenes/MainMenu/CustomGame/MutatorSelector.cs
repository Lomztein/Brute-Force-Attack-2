using Lomztein.BFA2.ContentSystem;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Scenes.Battlefield.Mutators;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class MutatorSelector : MonoBehaviour, ICustomGameAspectController
    {
        public Transform EnabledParent;
        public Transform AvailableParent;

        public GameObject EnabledPrefab;
        public GameObject AvailablePrefab;

        private readonly List<Mutator> _allMutators = new List<Mutator>();
        public bool DisableIfNone;

        private IEnumerable<AvailableMutator> AvailableMutators ()
        {
            foreach (Transform child in AvailableParent)
            {
                yield return child.GetComponent<AvailableMutator>();
            }
        }

        private IEnumerable<EnabledMutator> EnabledMutators()
        {
            foreach (Transform child in EnabledParent)
            {
                yield return child.GetComponent<EnabledMutator>();
            }
        }

        private AvailableMutator GetAvailableMutator(string identifier) => AvailableMutators().FirstOrDefault(x => x.Mutator.Identifier == identifier);
        private EnabledMutator GetEnabledMutator(string identifier) => EnabledMutators().FirstOrDefault(x => x.Mutator.Identifier == identifier);

        private void Awake()
        {
            _allMutators.AddRange(LoadMutators());
        }

        private void Start()
        {
            InitializeAvailableList();
            UpdateListHidden();
        }

        private void InitializeAvailableList ()
        {
            foreach (Mutator mutator in _allMutators)
            {
                if (GetAvailableMutator(mutator.Identifier) == null)
                {
                    CreateAvailableButton(mutator).Assign(mutator, EnableMutator);
                }
            }
        }

        public void AddMutator(Mutator mutator)
        {
            _allMutators.Add(mutator);
            CreateAvailableButton(mutator).Assign(mutator, EnableMutator);
            UpdateListHidden();
        }

        public void UpdateListHidden ()
        {
            if (DisableIfNone)
            {
                if (_allMutators.Count == 0)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }
        }

        public void RemoveMutator (string identifier)
        {
            // Disable it if it is enabled.
            EnabledMutator enabled = GetEnabledMutator(identifier);
            if (enabled)
            {
                DisableMutator(enabled.Mutator);
            }
            // Destroy the button.
            Destroy(GetAvailableMutator(identifier).gameObject);

            // Remove from list.
            _allMutators.RemoveAll(x => x.Identifier == identifier);
        }

        private AvailableMutator CreateAvailableButton(Mutator mutator)
        {
            GameObject newButton = Instantiate(AvailablePrefab, AvailableParent);
            AvailableMutator available = newButton.GetComponent<AvailableMutator>();
            return available;
        }

        private EnabledMutator CreateEnabledButton(Mutator mutator)
        {
            GameObject newButton = Instantiate(EnabledPrefab, EnabledParent);
            EnabledMutator enabled = newButton.GetComponent<EnabledMutator>();
            return enabled;
        }

        private void EnableMutator(Mutator obj)
        {
            Destroy(GetAvailableMutator(obj.Identifier).gameObject);
            CreateEnabledButton(obj).Assign(obj, DisableMutator);
            BattlefieldSettings.CurrentSettings.AddMutator(obj);

            LayoutRebuilder.ForceRebuildLayoutImmediate(EnabledParent as RectTransform);
        }

        private void DisableMutator (Mutator obj)
        {
            Destroy(GetEnabledMutator(obj.Identifier).gameObject);
            CreateAvailableButton(obj).Assign(obj, EnableMutator);
            BattlefieldSettings.CurrentSettings.RemoveMutator(obj);
        }

        private Mutator[] LoadMutators()
        {
            return Content.GetAll<Mutator>("*/Mutators");
        }

        public void ApplyBattlefieldSettings(BattlefieldSettings settings)
        {
            Mutator[] mutators = settings.Mutators;
            foreach (Mutator mutator in mutators)
            {
                settings.RemoveMutator(mutator);
                EnableMutator(mutator);
            }
            // It's not the cleanest but it works.
        }
    }
}