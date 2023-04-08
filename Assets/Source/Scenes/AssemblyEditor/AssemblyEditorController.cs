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
        public TierDisplay TierDisplay;

        public InputField NameText;
        public InputField TierText;
        public InputField DescriptionText;
        public StatSheet StatDisplay;

        public static event Action<TurretAssembly, string> OnAssemblyLoaded;
        public static event Action<TurretAssembly, string> OnAssemblySaved;

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
            TierDisplay.Clear();
            if (CurrentAsssembly)
            {
                Destroy(CurrentAsssembly.gameObject);
            }
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

            NameText.text = CurrentAsssembly.Name;
            DescriptionText.text = CurrentAsssembly.Description;
            TierText.text = WorkingTier.Name;
        }

        public void SaveAssembly ()
        {
            CustomContentUtils.CreateDirectory("Assemblies");
            string path = CustomContentUtils.ToAbsolutePath("Assemblies/" + CurrentAsssembly.Name + ".json");
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
            assembly.Identifier = Guid.NewGuid().ToString();
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            TurretAssemblyAssembler assembler = new TurretAssemblyAssembler();
            var model = assembler.Disassemble(assembly);
            File.WriteAllText(path, ObjectPipeline.SerializeObject(model).ToString());
            OnAssemblySaved?.Invoke(assembly, path);
            Content.ResetIndex();
            Alert.Open("Assembly has been saved.");
        }

        private void LoadFile(string path)
        {
            DeleteCurrentAssembly();

            var json = JObject.Parse(File.ReadAllText(path));
            var model = ObjectPipeline.DeserializeObject(json);

            TurretAssemblyAssembler assembler = new TurretAssemblyAssembler();
            CurrentAsssembly = assembler.Assemble(model);

            CurrentAsssembly.transform.position = Vector3.zero;
            CurrentAsssembly.transform.rotation = Quaternion.identity;

            NameText.text = CurrentAsssembly.Name;
            DescriptionText.text = CurrentAsssembly.Description;

            foreach (Transform tierParent in CurrentAsssembly.transform)
            {
                TierDisplay.AddTierButton(tierParent, Tier.Parse(tierParent.name));
            }

            OnAssemblyLoaded?.Invoke(CurrentAsssembly, path);
            TierDisplay.UpdateUpgradePaths();
            SetTier(Tier.Initial);
        }

        public void Load ()
        {
            Confirm.Open("Loading an assembly will trash unsaved progress on current assembly.\nConfirm?", () =>
            {
                CustomContentUtils.CreateDirectory("Assemblies");
                FileBrowser.Create("Select Assembly File", CustomContentUtils.ToAbsolutePath("Assemblies"), ".json", LoadFile);
            });
        }

        public void UpdateAssemblyName() => CurrentAsssembly.Name = NameText.text;
        public void UpdateTierName() => CurrentAsssembly.SetTierName(CurrentAsssembly.CurrentTeir, TierText.text);
        public void UpdateAssemblyDescription() => CurrentAsssembly.Description = DescriptionText.text;

        public bool IsTierEmpty(Tier tier) => CurrentAsssembly.GetRootComponent(tier) == null;
        public bool IsAssemblyEmpty() => CurrentAsssembly.transform.childCount == 0;

        public void SetTier (Tier tier)
        {
            CurrentAsssembly.SetTier(tier);
            StatDisplay.SetTarget(GetWorkingTierParent().gameObject);
            TierText.text = tier.Name;
        }

        public Transform GetTierParent(Tier tier) => CurrentAsssembly.GetTierParent(tier);
        public Transform GetWorkingTierParent() => GetTierParent(WorkingTier);

        public Tier AddTier (Tier tier)
        {
            GameObject newTier = new GameObject();
            CurrentAsssembly.AddTier(newTier.transform, tier);
            newTier.transform.SetParent(CurrentAsssembly.transform);
            TierDisplay.AddTierButton(newTier.transform, tier);
            return tier;
        }

        public void MoveTier (Tier from, Tier to)
        {

        }

        public void AddUpgradeOption(Tier from, Tier to)
        {
            CurrentAsssembly.UpgradeMap.AddTierToUpgradeOptions(from, to);
            TierDisplay.UpdateUpgradePaths();
        }

        public void RemoveUpgradeOption(Tier from, Tier to)
        {
            CurrentAsssembly.UpgradeMap.RemoveTierFromUpgradeOptions(from, to);
            TierDisplay.UpdateUpgradePaths();
        }

        public void ClearDeadUpgradeOptions ()
        {
            foreach (var option in CurrentAsssembly.UpgradeMap.Options)
            {
                var copy = new List<Tier>(option.NextTiers);

                // If current tier doesn't exist, then remove all outgoing upgrades.
                if (!CurrentAsssembly.HasTier(option.Tier))
                {
                    foreach (Tier tier in copy)
                    {
                        option.RemoveTier(tier);
                    }
                }

                // If outgoing tier doesn't exist, remove outgoing upgrade.
                foreach (Tier tier in copy)
                {
                    if (!CurrentAsssembly.HasTier(tier))
                    {
                        option.RemoveTier(tier);
                    }
                }
            }
        }

        public void RemoveTier (Tier tier)
        {
            if (tier.Equals(WorkingTier))
            {
                SetTier(tier);
            }

            TierDisplay.RemoveTierButton(tier);
            Transform parent = CurrentAsssembly.GetTierParent(tier);
            CurrentAsssembly.RemoveTier(tier);

            ClearDeadUpgradeOptions();
            TierDisplay.UpdateUpgradePaths();

            Destroy(parent.gameObject);
        }
    }
}
