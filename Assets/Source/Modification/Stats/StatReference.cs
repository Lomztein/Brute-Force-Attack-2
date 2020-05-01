using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public class StatReference : IStatReference
    {
        private IStat _stat;

        public StatReference (IStat stat)
        {
            _stat = stat;
        }

        public float GetValue ()
        {
            return _stat.GetValue();
        }
    }
}