using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    [System.Serializable]
    public class VisitMainMenuWindowRequirement : AchievementRequirement
    {
        [ModelProperty]
        public string WindowToVisit;
        private bool _visitedWindow;

        public override float Progression => _visitedWindow ? 1f : 0f;
        public override bool Completed => _visitedWindow;
        public override bool Binary => true;

        public override void End()
        {
            Facade.MainMenu.OnWindowChanged += OnWindowChanged;
        }

        private void OnWindowChanged(MainMenu.MenuWindow arg1, MainMenu.MenuWindow arg2)
        {
            if (arg2.Name == WindowToVisit)
            {
                _onCompletedCallback();
                _visitedWindow = true;
            }
        }

        public override void Init()
        {
            Facade.MainMenu.OnWindowChanged += OnWindowChanged;
        }
    }
}
