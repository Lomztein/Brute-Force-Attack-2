using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Audio
{
    [CreateAssetMenu(fileName = "New MusicState", menuName = "BFA2/Audio/Music State")]
    public class MusicState : ScriptableObject
    {
        [ModelProperty] public string Identifier;
        [ModelProperty] public string TrackPath;

        public IEnumerable<AudioClip> LoadTracks ()
        {
            return Content.GetAll<AudioClip>(TrackPath);
        }
    }
}
