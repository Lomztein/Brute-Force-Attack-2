using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentRequest : IEnumerator
    {
        public bool Completed => ReturnValue != null;
        public object ReturnValue { get; set; }
        public float Progress { get; private set; }
        public object Current => null;

        public bool MoveNext()
        {
            return ReturnValue != null;
        }

        public void Reset () { }

        public void SetProgress(int current, int total)
        {
            Progress = (float)current / total;
        }
    }
}
