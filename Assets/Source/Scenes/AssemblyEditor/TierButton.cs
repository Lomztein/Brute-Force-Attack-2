using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.AssemblyEditor
{
    public class TierButton : MonoBehaviour, IPointerClickHandler
    {
        public Tier Tier { get; private set; }
        public Transform TierParent { get; private set; }

        private Action<TierButton> _leftPointerCallback;
        private Action<TierButton> _rightPointerCallback;
        private Action<TierButton, TierButtonUpgradeArrow> _upgradeArrowCallback;

        public Button Button;
        public Image Image;

        public TierButtonUpgradeArrow[] UpgradeArrows;

        public void Assign(Transform tierParent, Tier tier, Action<TierButton> leftCallback, Action<TierButton> rightCallback, Action<TierButton, TierButtonUpgradeArrow> upgradeArrowCallback)
        {
            TierParent = tierParent;
            Tier = tier;
            _leftPointerCallback = leftCallback;
            _rightPointerCallback = rightCallback;
            _upgradeArrowCallback = upgradeArrowCallback;

            foreach (TierButtonUpgradeArrow arrow in UpgradeArrows)
            {
                Button button = arrow.GetComponent<Button>();
                button.onClick.AddListener(() => OnUpgradeButtonClick(arrow));
            }
        }

        private void OnUpgradeButtonClick (TierButtonUpgradeArrow arrow)
        {
            _upgradeArrowCallback(this, arrow);
        }

        private void Start()
        {
            Button.onClick.AddListener(() => _leftPointerCallback(this));
        }

        private void Update()
        {
            Image.sprite = RenderSprite();
            Image.gameObject.SetActive(Image.sprite);
        }

        private Sprite RenderSprite ()
        {
            if (TierParent)
            {
                return Iconography.GenerateSprite(TierParent.gameObject);
            }
            else
            {
                return null;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right && !Tier.Equals(Tier.Initial))
            {
                _rightPointerCallback(this);
            }
        }
    }
}
