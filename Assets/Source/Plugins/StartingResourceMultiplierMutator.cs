using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Plugins
{
    public class StartingResourceMultiplierMutator : Mutator
    {
        [ModelProperty]
        public float CreditsMultiplier = 2f;
        [ModelProperty]
        public float ResearchMultiplier = 2f;

        private void Start()
        {
            IResourceContainer container = GetComponent<IResourceContainer>();
            container.SetResource(Resource.Credits, Mathf.RoundToInt(container.GetResource(Resource.Credits) * CreditsMultiplier));
            container.SetResource(Resource.Research, Mathf.RoundToInt(container.GetResource(Resource.Research) * ResearchMultiplier));
        }
    }
}
