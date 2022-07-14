using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class AsyncGet : CustomYieldInstruction
    {
        public override bool keepWaiting => !Completed;

        public bool Completed => ReturnValue != null;
        public object ReturnValue { get; private set; }
    }

    public class AsyncGet<T> : CustomYieldInstruction
    {
        public override bool keepWaiting => !Completed;

        public bool Completed => ReturnValue != null;
        public T ReturnValue { get; private set; }
    }
}
