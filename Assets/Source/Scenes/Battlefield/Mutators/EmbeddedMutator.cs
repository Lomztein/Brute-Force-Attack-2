using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public class EmbeddedMutator : MonoBehaviour
    {
        [ModelProperty]
        public Mutator Mutator;

        private void Start()
        {
            Mutator.Start();
        }
    }
}
