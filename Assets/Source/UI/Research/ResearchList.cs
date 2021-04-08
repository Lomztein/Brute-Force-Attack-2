using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Research.UI
{
    //TODO: Sequencing of this class is a bit all over the place, perhaps redo it sometime.
    public class ResearchList : MonoBehaviour
    {
        public ResearchController Controller;

        public Transform ResearchOptionParent;
        public Transform TrackedResearchParent;

        public GameObject ResearchButtonPrefab;
        public GameObject TrackedResearchPrefab;

        public Text AvailableOptionsCount;

        private IResourceContainer _resourceContainer;

        private bool IsOpen => ResearchOptionParent.gameObject.activeSelf;

        private void Start()
        {
            _resourceContainer = GetComponent<IResourceContainer>();
            _resourceContainer.OnResourceChanged += OnResourceChanged;

            Controller.OnResearchBegun += OnResearchBegun;
            Controller.OnResearchCancelled += OnReseachCancelled;
            Controller.OnResearchCompleted += OnResearchCompleted;
            Controller.OnResearchProgressed += OnResearchProgressed;

            UpdateAvailableCount();
        }

        private void OnResourceChanged(Resource arg1, int arg2, int arg3)
        {
            UpdateMenu();
        }

        private void OnResearchProgressed(ResearchOption obj)
        {
            UpdateMenu();
        }

        private void OnResearchCompleted(ResearchOption obj)
        {
            UpdateMenu();
            RegenerateButtons();
        }

        private void OnReseachCancelled(ResearchOption obj)
        {
            UpdateMenu();
            RegenerateButtons();
        }

        private void OnResearchBegun(ResearchOption obj)
        {
            UpdateMenu();
            RegenerateButtons();
        }

        private void UpdateMenu ()
        {
            UpdateAvailableCount();
            UpdateAllButtonAvailability();
        }
        
        private void UpdateAvailableCount ()
        {
            int amount = Controller.GetAvailable().Where(x => _resourceContainer.HasEnough(x.ResourceCost)).Count();
            AvailableOptionsCount.text = $"{amount} available research option(s)";
        }

        public void Open()
        {
            if (IsOpen)
            {
                Close();
            }

            ResearchOptionParent.gameObject.SetActive(true);
            RegenerateButtons();
        }

        public void Close()
        {
            ResearchOptionParent.gameObject.SetActive(false);
        }

        private bool ShouldButtonBeAvailable (ResearchOptionButton butt)
            => _resourceContainer.HasEnough(butt.Research.ResourceCost) && butt.Research.UniquePrerequisitesCompleted;

        private void UpdateAllButtonAvailability ()
        {
            foreach (Transform child in ResearchOptionParent)
            {
                ResearchOptionButton butt = child.GetComponent<ResearchOptionButton>();
                UpdateButtonAvailability(butt);
            }
        }

        private void UpdateButtonAvailability (ResearchOptionButton butt)
        {
            butt.UpdateButton(ShouldButtonBeAvailable(butt));
        }

        private void GenerateResearchButtons ()
        {
            foreach (Transform child in ResearchOptionParent)
            {
                Destroy(child.gameObject);
            }

            ResearchOption[] available = Controller.GetAvailable();
            ResourceComparer resourceComparer = new ResourceComparer();

            Array.Sort(available, new Comparison<ResearchOption>((x, y) => resourceComparer.Compare(x.ResourceCost, y.ResourceCost)));

            foreach (var option in available)
            {
                GameObject butt = Instantiate(ResearchButtonPrefab, ResearchOptionParent);
                ResearchOptionButton button = butt.GetComponent<ResearchOptionButton>();
                button.Assign(option);

                Button actualButton = butt.GetComponent<Button>();
                actualButton.interactable = ShouldButtonBeAvailable(button);

                actualButton.onClick.AddListener(() => OnReseachButtonClick(option));
            }

            UpdateAllButtonAvailability();
        }

        private void OnReseachButtonClick(ResearchOption option)
        {
            Controller.BeginResearch(option);
            RegenerateButtons();
        }

        private void RegenerateButtons ()
        {
            GenerateTrackerButtons();
            GenerateResearchButtons();
        }

        private void GenerateTrackerButtons ()
        {
            foreach (Transform child in TrackedResearchParent)
            {
                Destroy(child.gameObject);
            }

            ResearchOption[] inProgress = Controller.GetInProgress();
            foreach (var option in inProgress)
            {
                GameObject butt = Instantiate(TrackedResearchPrefab, TrackedResearchParent);
                ResearchTracker button = butt.GetComponent<ResearchTracker>();
                button.Assign(option);
            }
        }
    }
}