using Lomztein.BFA2.AssemblyEditor;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.AssemblyEditor
{
    public class TierDisplay : MonoBehaviour
    {
        public TurretAssembly Assembly => AssemblyEditorController.Instance.CurrentAsssembly;

        public int TierIndex;
        public Button TierButton;
        public Button RemoveButton;
        public Image TierImage;
        public Text TierText;

        public Button SwapUp;
        public Button SwapDown;

        public void Assign(int index)
        {
            TierIndex = index;
            UpdateAll();

            TierButton.onClick.AddListener(SetTierToThis);
            RemoveButton.onClick.AddListener(RemoveThisTier);
        }

        private void SetTierToThis () => AssemblyEditorController.Instance.SetTier(TierIndex);
        private void RemoveThisTier() => AssemblyEditorController.Instance.RemoveTier(TierIndex);

        public void SetTier(Transform tierParent, int tier)
        {
            TierIndex = tier;
            UpdateAll();
        }

        public void UpdateAll ()
        {
            UpdateUI();
            UpdateIcon();
        }

        public void UpdateUI()
        {
            SwapUp.interactable = TierIndex != 0;
            SwapDown.interactable = TierIndex != Assembly.TierAmount - 1;
            RemoveButton.interactable = Assembly.TierAmount != 1;
            TierButton.interactable = AssemblyEditorController.Instance.WorkingTier != TierIndex;

            TierText.text = $"Tier {TierIndex + 1}" + (TierIndex == 0 ? " (Initial)" : string.Empty);
        }

        public void UpdateIcon ()
        {
            TierImage.sprite = Iconography.GenerateSprite(Assembly.GetTierParent(TierIndex).gameObject);
        }

        public void DoSwapUp ()
        {
            AssemblyEditorController.Instance.SwapTiers(TierIndex, TierIndex - 1);
            if (AssemblyEditorController.Instance.WorkingTier == TierIndex)
            {
                AssemblyEditorController.Instance.SetTier(TierIndex - 1);
            }else if (AssemblyEditorController.Instance.WorkingTier == TierIndex - 1)
            {
                AssemblyEditorController.Instance.SetTier(TierIndex);
            }
            UpdateUI();
        }

        public void DoSwapDown ()
        {
            AssemblyEditorController.Instance.SwapTiers(TierIndex, TierIndex + 1);
            if (AssemblyEditorController.Instance.WorkingTier == TierIndex)
            {
                AssemblyEditorController.Instance.SetTier(TierIndex + 1);
            }
            else if (AssemblyEditorController.Instance.WorkingTier == TierIndex + 1)
            {
                AssemblyEditorController.Instance.SetTier(TierIndex);
            }
            UpdateUI();
        }
    }
}
