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
                throw new InvalidOperationException("Patching audio clips is currently not supported.");
            }

            //Assert.IsTrue(Path.GetExtension(path).Equals("mp3"), "Only MP3 files are supported at the moment.");
            using UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV);
            request.SendWebRequest();

            while (!request.isDone) { }
            return DownloadHandlerAudioClip.GetContent(request);
        }
    }
}
