using Lomztein.BFA2.ContentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Audio
{
    public class MusicState : ScriptableObject
    {
        public string Identifier;
        public string TrackPath;

        public IEnumerable<AudioClip> LoadTracks ()
        {
            return Content.GetAll<AudioClip>(TrackPath);
        }
    }
}
