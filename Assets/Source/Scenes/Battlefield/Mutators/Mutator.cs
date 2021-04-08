using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Mutators
{
    public abstract class Mutator
    {
        [ModelProperty]
        public string Name { get; set; }
        [ModelProperty]
        public string Description { get; set; }
        [ModelProperty]
        public string Identifier { get; set; }

        public abstract void Start();
        public void Stop() { }
    }
}
