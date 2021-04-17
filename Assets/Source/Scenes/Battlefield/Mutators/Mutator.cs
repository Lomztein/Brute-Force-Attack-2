using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public abstract class Mutator : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public string Identifier;

        public abstract void Start();
        public void Stop() { }
    }
}
