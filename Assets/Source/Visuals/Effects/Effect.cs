using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Visuals.Effects
{
    public abstract class Effect : MonoBehaviour
    {
        public abstract bool IsPlaying { get; }

        private Vector3 _localPosition;
        private Quaternion _localRotation;
        private Coroutine _recallCoroutine;

        private void Awake()
        {
            _localPosition = transform.localPosition;
            _localRotation = transform.localRotation;
        }

        public virtual void Play()
        {
            if (_recallCoroutine != null)
            {
                StopCoroutine(_recallCoroutine);
                _recallCoroutine = null;
            }
        }

        public abstract void Stop();

        public void Play(float time)
        {
            Play();
            Invoke(nameof(Stop), time);
        }

        public void Detatch()
        {
            transform.parent = null;
        }

        public void Attach(Transform parent)
        {
            transform.parent = parent;

            transform.localPosition = _localPosition;
            transform.localRotation = _localRotation;
        }

        public void Recall(Transform parent, float time)
        {
            _recallCoroutine = StartCoroutine(DelayedRecall(parent, time));
        }

        private IEnumerator DelayedRecall(Transform parent, float time)
        {
            yield return new WaitForSeconds(time);
            Attach(parent);
        }
    }
}