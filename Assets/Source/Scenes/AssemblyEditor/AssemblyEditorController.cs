using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Assemblers;
using Lomztein.BFA2.Scenes.AssemblyEditor;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI;
using Lomztein.BFA2.UI.Displays.Stats;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.AssemblyEditor
{
    // Suffering a bit from god-class syndrome, could probably be split.
    public class AssemblyEditorController : MonoBehaviour
    {
        public static AssemblyEditorController Instance;
        public GameObject AssemblyPrefab;

        public TurretAssembly CurrentAsssembly;
        public int WorkingTier => CurrentAsssembly.CurrentTeir;

        public Transform TierDisplayParent;
        public GameObject TierDisplayPrefab;
        private List<TierDisplay> _tierDisplays = new List<TierDisplay>();

        public InputField NameText;
        public InputField DescriptionText;
        public StatSheet StatDisplay;

        public void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StartNewAssembly();
        }

        public void DeleteCurrentAssembly ()
        {
            foreach (TierDisplay display in _tierDisplays)
            {
                Destroy(display.gameObject);
            }
            _tierDisplays.Clear();
            Destroy((CurrentAsssembly as Component).gameObject);
            CurrentAsssembly = null;
        }

        public TurretAssembly InstantiateNewAssembly ()
        {
            GameObject newAssembly = Instantiate(AssemblyPrefab, Vector3.zero, Quaternion.identity);
            return newAssembly.GetComponent<TurretAssembly>();
        }

        public void StartNewAssembly ()
        {
            if (CurrentAsssembly)
            {
                DeleteCurrentAssembly();
            }
            SetAssembly(InstantiateNewAssembly());
            SetTier(AddTier());
        }

        public void OnClickNewAssembly ()
        {
            if (!IsAssemblyEmpty())
            {
                Confirm.Open("Creating a new assembly will trash unsaved progress on current assembly.\nConfirm?", StartNewAssembly);
            }
            else
            {
                StartNewAssembly();
            }
        }

        public void SetAssembly (TurretAssembly assembly)
        {
            CurrentAsssembly = assembly;
        }

        public void SaveAssembly ()
        {
            string path = Path.Combine(Content.CustomContentPath, "Assemblies", CurrentAsssembly.Name) + ".json";
            if (File.Exists(path))
            {
                Confirm.Open("Saving will overwrite assembly file\n" + path + "\nConfirm?", () =>
                {
                    SaveFile(path, CurrentAsssembly);
                });
            }
            else
            {
                SaveFile(path, CurrentAsssembly);
            }
        }

        public static void SaveFile(string path, TurretAssembly assembly)
        {
            TurretAssemblyAssembler assembler = new TurretAssemblyAssembler();
            var model = assembler.Disassemble(assembly);
            File.WriteAllText(path, ObjectPipeline.SerializeObject(model).ToString());
            Alert.Open("Assembly has been saved.");
        }

        private void LoadFile(string path)
        {
            if (CurrentAsssembly)
            {
                DeleteCurrentAssembly();
            }

            var json = JObject.Parse(File.ReadAllText(path));
            var model = ObjectPipeline.DeserializeObject(json);

            TurretAssemblyAssembler assembler = new TurretAssemblyAssembler();
            CurrentAsssembly = assembler.Assemble(model);

            if (CurrentAsssembly is Component comp)
            {
                comp.transform.position = Vector3.zero;
                comp.transform.rotation = Quaternion.identity;
            }

            for (int i = 0; i < CurrentAsssembly.TierAmount; i++)
            {
                Transform tierParent = GetTierParent(i);
                CreateNewTierDisplay(tierParent, i);
            }

            NameText.text = CurrentAsssembly.Name;
            DescriptionText.text = CurrentAsssembly.Description;
        }

        public void Load ()
        {
            Confirm.Open("Loading an assembly will trash unsaved progress on current assembly.\nConfirm?", () =>
            {
                FileBrowser.Create(Path.Combine(Content.CustomContentPath, "Assemblies"), ".json", LoadFile);
            });
        }

        public void UpdateAssemblyName() => CurrentAsssembly.Name = NameText.text;
        public void UpdateAssemblyDescription() => CurrentAsssembly.Description = DescriptionText.text;

        public bool IsTierEmpty(int tier) => CurrentAsssembly.GetRootComponent(tier) == null;
        public bool IsAssemblyEmpty() => Enumerable.Range(0, CurrentAsssembly.TierAmount).All(x => IsTierEmpty(x));

        public void SetTier (int tier)
        {
            CurrentAsssembly.SetTier(tier);
            UpdateAllTierDisplays();
            StatDisplay.SetTarget(GetWorkingTierParent().gameObject);
        }

        public Transform GetTierParent(int tier) => CurrentAsssembly.GetTierParent(tier);
        public Transform GetWorkingTierParent() => GetTierParent(WorkingTier);

        public int AddTier ()
        {
            GameObject newTier = new GameObject();
            CurrentAsssembly.AddTier(newTier.transform);
            newTier.transform.SetParent(CurrentAsssembly.transform);
            int tier = CurrentAsssembly.TierAmount - 1;
            CreateNewTierDisplay(newTier.transform, tier);
            UpdateAllTierDisplays();
            return tier;
        }

        public void RemoveTier (int tier)
        {
            if (tier == WorkingTier)
            {
                SetTier(0);
            }

            Transform parent = CurrentAsssembly.GetTierParent(tier);
            CurrentAsssembly.RemoveTier(tier);
            Destroy(parent.gameObject);
            TierDisplay td = GetTierDisplay(tier);

            Destroy (td.gameObject);
            _tierDisplays.Remove(td);

            foreach (TierDisplay display in _tierDisplays)
            {
                if (display.TierIndex > tier)
                {
                    display.SetTier(GetTierParent(display.TierIndex - 1), display.TierIndex - 1);
                }
            }

            UpdateAllTierDisplays();
        }

        public TierDisplay GetTierDisplay(int tier) => _tierDisplays.Find(x => x.TierIndex == tier);
        public void UpdateTierDisplay(int tier) => GetTierDisplay(tier).UpdateAll();
        public void UpdateAllTierDisplays() => _tierDisplays.ForEach(x => x.UpdateAll());

        public void AddTierFromButton ()
        {
            int newTier = AddTier();
            SetTier(newTier);
        }

        public TierDisplay CreateNewTierDisplay(Transform tierParent, int tier)
        {
            GameObject newObject = Instantiate(TierDisplayPrefab, TierDisplayParent);
            TierDisplay newDisplay = newObject.GetComponent<TierDisplay>();
            _tierDisplays.Add(newDisplay);
            newDisplay.Assign(tier);
            return newDisplay;
        }

        public void SwapTiers (int tier0, int tier1)
        {
            Transform tier0Parent = CurrentAsssembly.GetTierParent(tier0);
            Transform tier1Parent = CurrentAsssembly.GetTierParent(tier1);

            if (tier0 < tier1)
            {
                CurrentAsssembly.InsertTier(tier1, tier0Parent);
                CurrentAsssembly.RemoveTier(tier1 + 1);
                CurrentAsssembly.RemoveTier(tier0);
                CurrentAsssembly.InsertTier(tier0, tier1Parent);
            }
            else
            {
                CurrentAsssembly.RemoveTier(tier0);
                CurrentAsssembly.InsertTier(tier0, tier1Parent);
                CurrentAsssembly.RemoveTier(tier1);
                CurrentAsssembly.InsertTier(tier1, tier0Parent);
            }

            UpdateAllTierDisplays();
            // ..idk
            // could probably be a part of TurretAssembly.
        }
    }
}
