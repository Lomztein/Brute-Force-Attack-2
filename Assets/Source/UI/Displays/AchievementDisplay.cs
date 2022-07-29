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
        public Text DescriptionText;
        public Image Image;

        private AchievementManager _subscribedInstance;

        public static Achievement CurrentShown { get; private set; }
        private static float _showTime; // It's showtime, baby.
        private static Queue<Achievement> _toShow = new Queue<Achievement>();

        private void Awake()
        {
            _subscribedInstance = AchievementManager.Instance;
            _subscribedInstance.OnAchievementCompleted += OnAchievementCompleted;
        }

        private void OnAchievementCompleted(Achievement obj)
        {
            Enqueue(obj);
        }

        public void Enqueue(Achievement achievement)
        {
            _toShow.Enqueue(achievement);
        }

        private void OnDestroy()
        {
            _subscribedInstance.OnAchievementCompleted -= OnAchievementCompleted;
        }

        private void Show (Achievement achievement)
        {
            CurrentShown = achievement;
            _showTime = ShowTime;
        }

        private Vector3 GetTargetPosition()
            => CurrentShown == null ? HidePosition : ShowPosition;

        private void EndShow()
        {
            CurrentShown = null;
        }

        private void Update()
        {
            Transform.anchoredPosition = Vector3.Lerp(Transform.anchoredPosition, GetTargetPosition(), LerpTime * Time.deltaTime);

            if (CurrentShown == null && _toShow.Count > 0)
            {
                Show(_toShow.Dequeue());
            }
            if (CurrentShown != null)
            {
                NameText.text = CurrentShown.Name;
                DescriptionText.text = CurrentShown.Description;
                Image.sprite = CurrentShown.Sprite.Get();

                _showTime -= Time.deltaTime;
                if (_showTime < 0f)
                {
                    EndShow();
                }
            }
        }
    }
}
