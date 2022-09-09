using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class AudioLoaderStrategy : IContentLoaderStrategy
    {
        public bool CanLoad(Type type)
            => type == typeof(AudioClip);

        public object Load(string path, Type type, IEnumerable<string> patches)
        {
            if (patches.Count() > 0)
            {
                throw new InvalidOperationException("Patching audio clips is not supported.");
            }

            using UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, ExtensionToAudioType(Path.GetExtension(path)));
            request.SendWebRequest();

            while (!request.isDone) { }
            return DownloadHandlerAudioClip.GetContent(request);
        }

        private AudioType ExtensionToAudioType(string ext)
        {
            return ext switch
            {
                ".mp3" or ".mp2" => AudioType.MPEG,
                ".wav" => AudioType.WAV,
                ".ogg" => AudioType.OGGVORBIS,
                _ => throw new InvalidOperationException($"Extension '{ext}' not supported for audio clips."),
            };
        }
    }
}
