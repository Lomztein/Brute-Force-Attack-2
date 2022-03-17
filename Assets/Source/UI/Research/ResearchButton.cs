using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.Research;
using Lomztein.BFA2.UI.Windows;
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
    public class ResearchButton : MonoBehaviour
    {
        public ResearchController Controller;
        public ResearchTree ResearchTree;

        public Text AvailableOptionsCount;
        private IResourceContainer _resourceContainer;

        private void Start()
        {
            _resourceContainer = GetComponent<IResourceContainer>();
            _resourceContainer.OnResourceChanged += OnResourceChanged;

            Controller.OnResearchBegun += OnResearchBegun;
            Controller.OnResearchCancelled += OnReseachCancelled;
            Controller.OnResearchCompleted += OnResearchCompleted;
            Controller.OnResearchProgressed += OnResearchProgressed;

            UpdateAvailableCount();

            StartCoroutine(ResearchTree.GenerateTree());
        }

        private void OnResourceChanged(Resource arg1, int arg2, int arg3)
        {
            UpdateAvailableCount();
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
    }
}