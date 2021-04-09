using Lomztein.BFA2.Player.Progression.Achievements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays
{
    public class AchievementDisplay : MonoBehaviour
    {
        public RectTransform Transform;

        public Vector3 ShowPosition;
        public Vector3 HidePosition;
        public float LerpTime = 10;
        public float ShowTime = 5f;

        public Text NameText;
        public Image Image;

        private AchievementManager _subscribedInstance;
        private Vector3 _targetPosition;

        private void Start()
        {
            _subscribedInstance = AchievementManager.Instance;
            _subscribedInstance.OnAchievementCompleted += OnAchievementCompleted;
            Hide();
        }

        private void OnAchievementCompleted(Achievement obj)
        {
            Show(obj);
        }

        private void OnDestroy()
        {
            _subscribedInstance.OnAchievementCompleted -= OnAchievementCompleted;
        }

        private void Show (Achievement achievement)
        {
            _targetPosition = ShowPosition;

            NameText.text = achievement.name;
            Image.sprite = achievement.Sprite.Get();

            Invoke(nameof(Hide), ShowTime);
        }

        private void Hide ()
        {
            _targetPosition = HidePosition;
        }

        private void Update()
        {
            Transform.anchoredPosition = Vector3.Lerp(Transform.anchoredPosition, _targetPosition, LerpTime * Time.deltaTime);
        }
    }
}
