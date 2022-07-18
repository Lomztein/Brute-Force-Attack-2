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
                .Where(x => PrerequisitesCompleted(x)) // Filter unfinished prerequisites.
                .Where(x => x.UniquePrerequisitesCompleted) // Filter with own unfinished prerequisites.
                .ToArray();
        }

        public ResearchOption[] GetAll() => _all.ToArray();

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

        private bool PrerequisitesCompleted (ResearchOption option)
        {
            return option.PrerequisiteIdentifiers.All(x => _completed.Exists(y => y.Identifier == x));
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