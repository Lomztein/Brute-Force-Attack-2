using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Player.Messages
{
    public class MajorMessageController : MonoBehaviour
    {
        public static MajorMessageController Instance;

        public Animator Animator;
        public Text Text;
        public float Margin = 0.5f;

        private readonly Queue<Message> _messageQueue = new Queue<Message>();
        private bool _isActive;

        private void Awake()
        {
            Instance = this;
            End();
        }

        public void Enqueue(Message message)
        {
            _messageQueue.Enqueue(message);
            if (!_isActive)
            {
                Begin();
            }
        }

        private void Begin()
        {
            _isActive = true;
            gameObject.SetActive(true);
            StartCoroutine(Play());
        }

        private IEnumerator Play()
        {
            PopUp();
            while (_messageQueue.Count != 0)
            {
                Message message = _messageQueue.Dequeue();
                Text.text = message.Content;
                yield return message.Wait();
            }
            PopOut();
            yield return new WaitForSeconds(Margin);
            End();

            if (_messageQueue.Count != 0)
            {
                // If a new message was added during popout, then this'll make sure they're displayed
                // Could technically result in a stack overflow, but that seems quite incredibly unlikely.
                Begin();
            }
        }

        private void End ()
        {
            _isActive = false;
            gameObject.SetActive(false);
        }

        public void PopUp ()
        {
            Animator.SetTrigger("PopIn");
        }

        public void PopOut ()
        {
            Animator.SetTrigger("PopOut");
        }
    }
}
