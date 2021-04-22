using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Research.UniquePrerequisites
{
    [Serializable]
    public abstract class UniquePrerequisite 
    {
        public abstract event Action<UniquePrerequisite> OnCompleted;
        public abstract event Action<UniquePrerequisite> OnProgressed;

        public abstract void Init();

        public abstract void Stop();

    }
}
