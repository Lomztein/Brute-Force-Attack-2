using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Research.Requirements
{
    public abstract class CompletionRequirement : MonoBehaviour
    {
        public abstract float Progress { get; }

        public abstract event Action<CompletionRequirement> OnCompleted;
        public abstract event Action<CompletionRequirement> OnProgressed;

        public abstract void Init();

        public abstract void Stop();
    }
}
