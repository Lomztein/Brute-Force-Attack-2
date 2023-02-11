using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lomztein.BFA2.Audio
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager _instance;

        public AudioSource Source;
        public float CrossFadeTime;
        public MusicState StartingState;

        public static MusicState CurrentState { get; private set; }
        private int _currentTrackIndex;
        private bool _crossfading;

        private float MusicVolume => Settings.Audio.MusicVolume;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            if (StartingState)
            {
                SetMusicState(StartingState, false);
            }
        }

        private void Update()
        {
            if (!_crossfading)
            {
                Source.volume = MusicVolume;
            }
        }

        public static void SetMusicState(MusicState state, bool crossfade)
        {
            CurrentState = state;
            _instance.Play(_instance.Source, state.LoadTracks().First(), crossfade);
        }

        private void Play(AudioSource source, AudioClip clip, bool crossfade)
        {
            float trackTime = clip.length;
            if (crossfade)
            {
                // Compensate for delayed start when using crossfade.
                // It occurs to me that this isn't actually a crossfade
                // but rather simply a sequential fade in / fade out
                // but whatever lol
                trackTime += CrossFadeTime / 2f;
                Crossfade(source, clip);
            }
            else
            {
                source.Stop();
                source.clip = clip;
                source.Play();
            }

            CancelInvoke(nameof(PlayRandomTrack));
            Invoke(nameof(PlayRandomTrack), trackTime);
        }

        private void PlayRandomTrack ()
        {
            var clips = CurrentState.LoadTracks().ToArray();
            Assert.IsTrue(clips.Length > 0, "There must be at least one audio clip in a music state.");
            
            if (clips.Length == 1)
            {
                _currentTrackIndex = 0;
            }
            else
            {
                _currentTrackIndex += Random.Range(0, clips.Length - 1) % clips.Length; // should avoid playing the same track twice.
            }

            Play(Source, clips[_currentTrackIndex], false);
        }

        private void Crossfade(AudioSource source, AudioClip clip)
        {
            StartCoroutine(InternalCrossfade(source, clip));
        }

        private IEnumerator InternalCrossfade(AudioSource source, AudioClip clip)
        {
            _crossfading = true;
            int steps = Mathf.RoundToInt(1f / CrossFadeTime / Time.fixedDeltaTime) / 2;
            // descend
            for (int i = 0; i < steps; i++)
            {
                source.volume = Mathf.Lerp(0, 1, 1f - (i / (float)steps)) * MusicVolume;
                yield return new WaitForFixedUpdate();
            }

            source.Stop();
            source.clip = clip;
            source.Play();

            // ascend
            for (int i = 0; i < steps; i++)
            {
                source.volume = Mathf.Lerp(0, 1, i / (float)steps) * MusicVolume;
                yield return new WaitForFixedUpdate();
            }
            _crossfading = false;
        }
    }
}
