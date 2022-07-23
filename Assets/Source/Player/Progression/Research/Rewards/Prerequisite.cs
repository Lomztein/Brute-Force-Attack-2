using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research
{
    [System.Serializable]
    public class Prerequisite
    {
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public bool Required;
    }
}
