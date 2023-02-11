using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Audio
{
    public class SFXManager : MonoBehaviour
    {
        private static SFXManager _instance;
        public float EffectsVolume => Settings.Audio.SFXVolume;

        public AudioSource EffectsSource;

        public int MaxSimultaniousEffects = 32;
        public int MaxSimultaniousEffectsOfType = 4;

        private Dictionary<AudioClip, int> _currentPlaying = new Dictionary<AudioClip, int>();
        private int _totalCurrentPlaying;

        private void Awake()
        {
            _instance = this;
        }

        private void Update()
        {
            EffectsSource.volume = EffectsVolume;
        }

        public static bool CanPlay(AudioClip clip)
        {
            if (_instance._totalCurrentPlaying >= _instance.MaxSimultaniousEffects)
            {
                return false;
            }
            else
            {
                if (_instance._currentPlaying.TryGetValue(clip, out int current))
                {
                    return current < _instance.MaxSimultaniousEffectsOfType;
                }
            }
            return true;
        }

        public static bool TryPlayOneShot(AudioClip clip)
        {
            if (CanPlay(clip))
            {
                _instance.StartCoroutine(_instance.InternalPlay(_instance.EffectsSource, clip, 1f));
                return true;
            }
            return false;
        }

        public void RegisterPlaying(AudioClip clip)
        {
            if (!_currentPlaying.ContainsKey(clip))
                _currentPlaying.Add(clip, 0);
            _currentPlaying[clip]++;
            _totalCurrentPlaying++;
        }

        public void UnregisterPlaying(AudioClip clip)
        {
            _currentPlaying[clip]--;
            _totalCurrentPlaying--;
        }

        private IEnumerator InternalPlay(AudioSource source, AudioClip clip, float scale)
        {

            float length = clip.length;
            source.PlayOneShot(clip, scale);
            RegisterPlaying(clip);
            yield return new WaitForSecondsRealtime(length);
            UnregisterPlaying(clip);
        }
    }
}
