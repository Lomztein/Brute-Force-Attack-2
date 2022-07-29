using Lomztein.BFA2.Research;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.FacadeComponents.Battlefield
{
    public class ResearchSubFacade : SceneFacadeSubComponent<BattlefieldFacade>
    {
        public ResearchController Controller;

        public event Action<ResearchOption> OnResearchBegun;
        public event Action<ResearchOption> OnResearchCompleted;
        public event Action<ResearchOption> OnResearchProgressed;
        public event Action<ResearchOption> OnResearchCancelled;
        public event Action<ResearchOption> OnResearchAdded;

        public override void OnSceneLoaded()
        {
            Controller = ResearchController.Instance;

            Controller.OnResearchBegun += OnResearchBegun;
            Controller.OnResearchCompleted += OnResearchCompleted;
            Controller.OnResearchProgressed += OnResearchProgressed;
            Controller.OnResearchCancelled += OnResearchCancelled;
            Controller.OnResearchAdded += OnResearchAdded;
        }

        public override void OnSceneUnloaded()
        {
            if (Controller)
            {
                Controller.OnResearchBegun -= OnResearchBegun;
                Controller.OnResearchCompleted -= OnResearchCompleted;
                Controller.OnResearchProgressed -= OnResearchProgressed;
                Controller.OnResearchCancelled -= OnResearchCancelled;
                Controller.OnResearchAdded -= OnResearchAdded;
            }

            Controller = null;
        }
    }
}
