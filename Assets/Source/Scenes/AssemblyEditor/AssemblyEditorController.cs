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
        public Tier WorkingTier => CurrentAsssembly.CurrentTeir;

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
            SetTier(AddTier(Tier.Initial));
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
            Directory.CreateDirectory(Path.GetDirectoryName(path));
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

        public bool IsTierEmpty(Tier tier) => CurrentAsssembly.GetRootComponent(tier) == null;
        public bool IsAssemblyEmpty() => CurrentAsssembly.transform.childCount == 0;

        public void SetTier (Tier tier)
        {
            CurrentAsssembly.SetTier(tier);
            StatDisplay.SetTarget(GetWorkingTierParent().gameObject);
        }

        public Transform GetTierParent(Tier tier) => CurrentAsssembly.GetTierParent(tier);
        public Transform GetWorkingTierParent() => GetTierParent(WorkingTier);

        public Tier AddTier (Tier tier)
        {
            GameObject newTier = new GameObject();
            CurrentAsssembly.AddTier(newTier.transform, tier);
            newTier.transform.SetParent(CurrentAsssembly.transform);
            return tier;
        }

        public void RemoveTier (Tier tier)
        {
            if (tier.Equals(WorkingTier))
            {
                SetTier(tier);
            }

            Transform parent = CurrentAsssembly.GetTierParent(tier);
            CurrentAsssembly.RemoveTier(tier);
            Destroy(parent.gameObject);
        }
    }
}
