using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References.PrefabProviders;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Research
{
    public class ResearchController : MonoBehaviour
    {
        public static ResearchController Instance;

        private ICachedPrefabProvider _provider;
        private IResourceContainer _resourceContainer;

        private List<ResearchOption> _all;
        private List<ResearchOption> _inProgress = new List<ResearchOption>();
        private List<ResearchOption> _completed = new List<ResearchOption>();

        public event Action<ResearchOption> OnResearchBegun;
        public event Action<ResearchOption> OnResearchCompleted;
        public event Action<ResearchOption> OnResearchProgressed;
        public event Action<ResearchOption> OnResearchCancelled;
        public event Action<int> OnMaxResearchSlotsSet;

        public int MaxResearchSlots = 1;

        public void SetMaxResearchSlots(int amount)
        {
            MaxResearchSlots = amount;
            OnMaxResearchSlotsSet?.Invoke(amount);
        }

        public ResearchOption[] GetInProgress() => _inProgress.ToArray();
        public ResearchOption[] GetCompleted() => _completed.ToArray();

        public ResearchOption[] GetAvailable()
        {
            return _all
                .Where(x => !_inProgress.Exists(y => y.Identifier == x.Identifier)) // Filter in progress.
                .Where(x => !_completed.Exists(y => y.Identifier == x.Identifier)) // Filter completed.
                .Where(x => PrerequisitesCompleted(x)) // Filter unfinished prerequisites.
                .ToArray();
        }

        private void Awake()
        {
            Instance = this;
            _provider = GetComponent<ICachedPrefabProvider>();
            _resourceContainer = GetComponent<IResourceContainer>();
            SetMaxResearchSlots(MaxResearchSlots);
        }

        private void Start()
        {
            _all = LoadResearchOptions();
        }

        private bool PrerequisitesCompleted (ResearchOption option)
        {
            return option.PrerequisiteIdentifiers.All(x => _completed.Exists(y => y.Identifier == x));
        }

        public void AddResearchOption (ResearchOption option)
        {
            SceneCachedGameObject _ = new SceneCachedGameObject(option.gameObject);
            _all.Add(option);
        }

        private List<ResearchOption> LoadResearchOptions()
        {
            return _provider.Get().Select(x => x.GetCache().GetComponent<ResearchOption>()).ToList();
        }

        private ResearchOption InstantiateOption(ResearchOption option)
        {
            ResearchOption opt = Instantiate(option.gameObject).GetComponent<ResearchOption>();
            opt.gameObject.SetActive(true);
            return opt;
        }

        public void BeginResearch(ResearchOption option)
        {
            if (_inProgress.Count < MaxResearchSlots && _resourceContainer.TrySpend(option.InitialCost))
            {
                ResearchOption copy = InstantiateOption(option);

                copy.OnCompleted += ResearchCompleted;
                copy.OnProgressed += ResearchProgressed;
                copy.Init();

                _inProgress.Add(copy);

                OnResearchBegun?.Invoke(copy);
            }
        }

        public void CancelResearch (ResearchOption option)
        {
            option.Stop();
            _inProgress.Remove(option);
            StopResearch(option);
            _resourceContainer.AddResources(option.InitialCost);

            OnResearchCancelled?.Invoke(option);
        }

        private void ResearchCompleted (ResearchOption option)
        {
            _completed.Add(option);
            _inProgress.Remove(option);
            StopResearch(option);

            OnResearchCompleted?.Invoke(option);
        }

        private void ResearchProgressed (ResearchOption option)
        {
            OnResearchProgressed?.Invoke(option);
        }

        private void StopResearch (ResearchOption option)
        {
            option.OnCompleted -= ResearchCompleted;
            option.OnProgressed -= ResearchProgressed;
        }
    }
}