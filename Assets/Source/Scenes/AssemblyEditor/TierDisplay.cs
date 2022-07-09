using Lomztein.BFA2.AssemblyEditor;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.AssemblyEditor
{
    public class TierDisplay : MonoBehaviour
    {
        public AssemblyEditorController Controller;
        public RectTransform ViewPort;

        public bool Expanded { get; private set; } = false;
        public RectTransform ViewPortSmallTarget;
        public RectTransform ViewPortLargeTarget;
        public float ExpandLerpSpeed;

        public Image ExpandButtonImage;
        public Sprite ExpandSprite;
        public Sprite ShrinkSprite;

        public RectTransform TierParent;

        public float GridSize;

        public GameObject TierButtonPrefab;
        public GameObject TierUpgradePathVizualiserPrefab;

        private List<TierButton> _tierButtons = new List<TierButton>();
        private List<UpgradePathVisualizer> _tierUpgradePathVisualizers = new List<UpgradePathVisualizer>();

        public void Expand()
        {
            Expanded = true;
            ExpandButtonImage.sprite = ShrinkSprite;
        }

        public void Shrink()
        {
            Expanded = false;
            ExpandButtonImage.sprite = ExpandSprite;
        }

        public Vector3 SnapToGrid (Vector2 position)
        {
            float x = Mathf.RoundToInt(position.x / GridSize) * GridSize;
            float y = Mathf.RoundToInt(position.y / GridSize) * GridSize;
            return new Vector3(x, y);
        }

        public void ToggleExpand()
        {
            if (Expanded)
            {
                Shrink();
            }
            else
            {
                Expand();
            }
        }

        private void Update()
        {
            RectTransform target = Expanded ? ViewPortLargeTarget : ViewPortSmallTarget;
            ViewPort.sizeDelta = Vector2.Lerp(ViewPort.sizeDelta, target.sizeDelta, ExpandLerpSpeed * Time.deltaTime);
        }

        public Vector3 TierIndexToGridPosition (Tier tier)
        {
            return new Vector3(
                tier.VariantIndex * GridSize,
                -tier.TierIndex * GridSize
                );
        }

        public Tier GridPositionToTier (Vector3 gridPosition)
        {
            int variant = Mathf.RoundToInt(gridPosition.x / GridSize);
            int tier = -Mathf.RoundToInt(gridPosition.y / GridSize);
            return new Tier(tier, variant);
        }

        public Transform GetTierParentAtPosition (Vector3 gridPosition)
        {
            Tier atPos = GridPositionToTier(gridPosition);
            return Controller.CurrentAsssembly.GetTierParent(atPos);
        }

        public void AddTierButton(Transform tierParent, Tier tier)
        {
            GameObject newTierButton = Instantiate(TierButtonPrefab, TierParent);
            newTierButton.transform.position = TierIndexToGridPosition(tier) + TierParent.position;
            TierButton button = newTierButton.GetComponent<TierButton>();
            button.Assign(tierParent, tier, TierButtonCallback, TierButtonDeleteCallback, TierButtonUpgradeArrowCallback);
            _tierButtons.Add(button);
        }

        private void TierButtonCallback(TierButton button) => SetTier(button.Tier);

        private void TierButtonDeleteCallback(TierButton button)
        {
            Confirm.Open($"Are you sure you wish to delete this tier? ({button.Tier})", () => Controller.RemoveTier(button.Tier));
        }

        private void TierButtonUpgradeArrowCallback (TierButton button, TierButtonUpgradeArrow arrow)
        {
            Tier tier = new Tier("Unnamed Tier", button.Tier.TierIndex + arrow.TargetTier, button.Tier.VariantIndex + arrow.TargetVariant);
            var tierParent = Controller.CurrentAsssembly.GetTierParent(tier);
            if (tierParent == null)
            {
                Controller.AddTier(tier);
                Controller.AddUpgradeOption(button.Tier, tier);
            }
            else
            {
                Controller.AddUpgradeOption(button.Tier, tier);
            }
            UpdateUpgradePaths();
        }

        public void RemoveTierButton (Tier tier)
        {
            TierButton button = _tierButtons.FirstOrDefault(x => x.Tier.Equals(tier));
            if (button != null)
            {
                Destroy(button.gameObject);
                _tierButtons.Remove(button);
            }
            UpdateUpgradePaths();
        }

        public void SetTier (Tier tier)
        {
            Controller.SetTier(tier);
        }

        public void UpdateUpgradePaths ()
        {
            foreach (var visualizer in _tierUpgradePathVisualizers)
            {
                Destroy(visualizer.gameObject);
            }
            _tierUpgradePathVisualizers.Clear();

            foreach (Transform tierParent in Controller.CurrentAsssembly.transform)
            {
                Tier tier = Tier.Parse(tierParent.name);
                var options = Controller.CurrentAsssembly.UpgradeMap.GetUpgradeOptions(tier);
                if (options != null && options.NextTiers != null)
                {
                    foreach (Tier upgradeOption in options.NextTiers)
                    {
                        Vector3 fromPos = TierIndexToGridPosition(tier) + TierParent.position;
                        Vector3 toPos = TierIndexToGridPosition(upgradeOption) + TierParent.position;

                        Vector3 midpoint = Vector3.Lerp(fromPos, toPos, 0.5f);
                        float angle = Mathf.Rad2Deg * Mathf.Atan2(toPos.y - fromPos.y, toPos.x - fromPos.x);

                        GameObject newVisualizer = Instantiate(TierUpgradePathVizualiserPrefab, TierParent);
                        newVisualizer.transform.position = midpoint;
                        newVisualizer.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                        
                        RectTransform newVisualizerRect = newVisualizer.transform as RectTransform;
                        newVisualizerRect.sizeDelta = new Vector2(Vector3.Distance(fromPos, toPos) - 64, newVisualizerRect.sizeDelta.y);

                        newVisualizer.transform.SetAsFirstSibling();

                        var visualizer = newVisualizer.GetComponent<UpgradePathVisualizer>();
                        visualizer.Assign(tier, upgradeOption, UpgradePathVisualizerCallback);
                        _tierUpgradePathVisualizers.Add(visualizer);
                    }
                }
            }

            foreach (TierButton button in _tierButtons)
            {
                foreach (TierButtonUpgradeArrow arrow in button.UpgradeArrows)
                {
                    Tier tier = new Tier(button.Tier.TierIndex + arrow.TargetTier, button.Tier.VariantIndex + arrow.TargetVariant);
                    var tierParent = Controller.CurrentAsssembly.GetTierParent(tier);

                    if (tierParent == null || !Controller.CurrentAsssembly.UpgradeMap.GetNext(button.Tier).Contains(tier))
                    {
                        arrow.gameObject.SetActive(true);
                    }
                    else
                    {
                        arrow.gameObject.SetActive(false);
                    }
                }
            }
        }

        private void UpgradePathVisualizerCallback(UpgradePathVisualizer visualizer)
        {
            Controller.RemoveUpgradeOption(visualizer.From, visualizer.To);
        }

        public void Clear ()
        {
            foreach (TierButton button in _tierButtons)
            {
                Destroy(button.gameObject);
            }
            foreach (UpgradePathVisualizer visualizer in _tierUpgradePathVisualizers)
            {
                Destroy(visualizer.gameObject);
            }
            _tierButtons.Clear();
            _tierUpgradePathVisualizers.Clear();
        }
    }
}
