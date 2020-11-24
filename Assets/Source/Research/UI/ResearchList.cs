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
            UpdateAvailableCount();
            UpdateButtonAvailability();
        }

        private void OnResearchProgressed(ResearchOption obj)
        {
            UpdateAvailableCount();
        }

        private void OnResearchCompleted(ResearchOption obj)
        {
            UpdateAvailableCount();
        }

        private void OnReseachCancelled(ResearchOption obj)
        {
            UpdateAvailableCount();
        }

        private void OnResearchBegun(ResearchOption obj)
        {
            UpdateAvailableCount();
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
            GenerateResearchButtons();
            UpdateButtonAvailability();
        }

        public void Close()
        {
            ResearchOptionParent.gameObject.SetActive(false);

            GenerateTrackerButtons();
        }

        private void UpdateButtonAvailability ()
        {
            foreach (Transform child in ResearchOptionParent)
            {
                ResearchOptionButton butt = child.GetComponent<ResearchOptionButton>();
                butt.UpdateAffordability(_resourceContainer);
            }
        }

        private void GenerateResearchButtons ()
        {
            foreach (Transform child in ResearchOptionParent)
            {
                Destroy(child.gameObject);
            }

            ResearchOption[] available = Controller.GetAvailable();
            Array.Sort(available, new Comparison<ResearchOption>((x, y) => _resourceContainer.HasEnough(x.ResourceCost) ? 1 : -1));

            foreach (var option in available)
            {
                GameObject butt = Instantiate(ResearchButtonPrefab, ResearchOptionParent);
                ResearchOptionButton button = butt.GetComponent<ResearchOptionButton>();
                button.Assign(option);

                Button actualButton = butt.GetComponent<Button>();
                actualButton.onClick.AddListener(() => OnReseachButtonClick(option));
            }
        }

        private void OnReseachButtonClick(ResearchOption option)
        {
            Controller.BeginResearch(option);
            Refresh();
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

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !UIUtils.IsOverUI(Input.mousePosition))
            {
                Close();
            }
        }

        public void Refresh()
        {
            Close();
            Open();
        }
    }
}