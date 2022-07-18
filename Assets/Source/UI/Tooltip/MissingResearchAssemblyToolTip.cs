using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Research;
using Lomztein.BFA2.Structures.Turrets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class MissingResearchAssemblyToolTip : MonoBehaviour, IPurchasableToolTip
    {
        public Text Title;
        public RectTransform MissingResearchParent;
        public GameObject MissingResearchPrefab;
        public MissingResearchDisplay MissingResearch;

        private IUnlockList List => Player.Player.Unlocks;

        public void Assign(IPurchasable obj)
        {
            if (obj is TurretAssembly assembly)
            {
                Title.text = assembly.Name;
                MissingResearch.Display(List, assembly.GetComponents().Select(x => x.Identifier));
            }
        }
    }
}
