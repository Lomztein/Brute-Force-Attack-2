using Lomztein.BFA2.Player.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Research.UI
{
    public abstract class ResearchTracker : MonoBehaviour
    {
        public abstract float Progress { get; }
        public abstract string Status { get; }

        protected ResearchOption _option;

        public Image SpriteImage;
        public Text Text;

        public virtual void Assign (ResearchOption option)
        {
            SpriteImage.sprite = option.Sprite.Get();
            SpriteImage.color = option.SpriteTint;
            option.OnProgressed += OnProgressed;
            option.OnCompleted += OnCompleted;

            _option = option;
            UpdateText();
        }

        private void OnCompleted(ResearchOption obj)
        {
            Message.Send($"Research '{obj.Name}' has completed.", Message.Type.Minor);
        }

        private void OnProgressed(ResearchOption obj)
        {
            UpdateText();
        }

        public void UpdateText()
            => Text.text = Status;

        private void OnDestroy()
        {
            if (_option != null)
            {
                _option.OnProgressed -= OnProgressed; // Gotta clean up when dealing with events.
                _option.OnCompleted -= OnCompleted;
            }
        }
    }
}
