using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References.PrefabProviders;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Lomztein.BFA2.Enemies.Waves;

namespace Lomztein.BFA2.Research
{
    public class ResearchController : MonoBehaviour
    {
        private const string RESEARCH_PATH = "*/Research/*";

        public static ResearchController Instance;

        private IResourceContainer _resourceContainer;

        [SerializeField]
        private List<ResearchOption> _all;
        private List<ResearchOption> _inProgress = new List<ResearchOption>();
        private List<ResearchOption> _completed = new List<ResearchOption>();

        public event Action<ResearchOption> OnResearchBegun;
        public event Action<ResearchOption> OnResearchCompleted;
        public event Action<ResearchOption> OnResearchProgressed;
        public event Action<ResearchOption> OnResearchCancelled;
        public event Action<ResearchOption> OnResearchAdded;
        public event Action OnTick;

        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        public ResearchOption[] GetInProgress() => _inProgress.ToArray();
        public ResearchOption[] GetCompleted() => _completed.ToArray();

        public ResearchOption[] GetAvailable()
        {
            return _all
                .Where(x => !_inProgress.Exists(y => y.Identifier == x.Identifier)) // Filter in progress.
                .Where(x => !_completed.Exists(y => y.Identifier == x.Identifier)) // Filter completed.
                .Where(x => ArePrerequisitesResearched(x)) // Filter unfinished prerequisites.
                .Where(x => x.UniquePrerequisitesCompleted) // Filter with own unfinished prerequisites.
                .ToArray();
        }

        public ResearchOption[] GetAll() => _all.ToArray();

        public ResearchOption GetOption(string identifier) => _all.FirstOrDefault(x => x.Identifier == identifier);

        public IEnumerable<ResearchOption> GetAllUnresearchedPrerequisites(ResearchOption option)
        {
            if (!IsCompleted(option.Identifier))
            {
                yield return option;
            }
            var prerequisites = option.Prerequisites.Select(x => GetOption(x.Identifier));
            foreach (var prerequisite in prerequisites)
            {
                var unresearched = GetAllUnresearchedPrerequisites(prerequisite);
                foreach (var missing in unresearched)
                {
                    yield return missing;
                }
            }
        }

        private void Awake()
        {
            Instance = this;
            _resourceContainer = GetComponent<IResourceContainer>();

            _all = LoadResearch().ToList();
            InitResearch(_all);
        }

        private void Start()
        {
            _roundController.IfExists((x) => x.OnWaveFinished += OnWaveFinished);
        }

        public bool IsCompleted(string identifier)
            => _completed.Any(x => x.Identifier == identifier);

        private void OnWaveFinished(int arg1, WaveHandler arg2)
        {
            List<ResearchOption> toTick = new List<ResearchOption>(_inProgress);

            foreach (ResearchOption option in toTick)
            {
                option.Tick();
            }

            OnTick?.Invoke();
        }

        private IEnumerable<ResearchOption> LoadResearch()
            => Content.GetAll<ResearchOption>(RESEARCH_PATH).Select(x => Instantiate(x));

        private void InitResearch (IEnumerable<ResearchOption> research) 
        {
            foreach (var option in research)
            {
                option.OnTick += ResearchProgressed;
                option.OnUniquePrerequisiteProgressed += ResearchProgressed;
                option.OnUniquePrerequisiteCompleted += ResearchProgressed;
                option.Init();
            }
        }

        public bool ArePrerequisitesResearched(ResearchOption option)
            => ArePrerequisitesResearched(option.Prerequisites);

        public bool ArePrerequisitesResearched(IEnumerable<Prerequisite> prerequisite)
        {
            var required = prerequisite.Where(x => x.Required);
            var optional = prerequisite.Where(x => !x.Required);
            bool allRequired = required.All(x => IsCompleted(x.Identifier)) || required.Count() == 0;
            bool anyOptional = optional.Any(x => IsCompleted(x.Identifier)) || optional.Count() == 0;
            return allRequired && anyOptional;
        }

        public void AddResearchOption (ResearchOption option)
        {
            _all.Add(option);
            option.Init();
            OnResearchAdded?.Invoke(option);
            Message.Send($"New research option {option.Name}' available.", Message.Type.Minor);
        }

        public void BeginResearch(ResearchOption option)
        {
            if (_resourceContainer.TrySpend(option.ResourceCost))
            {
                option.OnCompleted += ResearchCompleted;
                _inProgress.Add(option);

                option.BeginResearch();
                OnResearchBegun?.Invoke(option);
            }
        }

        public void CancelResearch (ResearchOption option)
        {
            _inProgress.Remove(option);
            option.OnCompleted -= ResearchCompleted;

            _resourceContainer.AddResources(option.ResourceCost);
            OnResearchCancelled?.Invoke(option);
        }

        private void ResearchCompleted (ResearchOption option)
        {
            _completed.Add(option);
            _inProgress.Remove(option);

            option.OnUniquePrerequisiteCompleted -= ResearchCompleted;
            Message.Send($"Research '{option.Name}' completed.", Message.Type.Minor);

            OnResearchCompleted?.Invoke(option);
        }

        private void ResearchProgressed (ResearchOption option)
        {
            OnResearchProgressed?.Invoke(option);
        }
    }
}