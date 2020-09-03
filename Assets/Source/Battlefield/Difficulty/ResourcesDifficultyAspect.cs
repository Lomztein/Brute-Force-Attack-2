using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Battlefield.Difficulty
{
    public class ResourcesDifficultyAspect : IDifficultyAspect
    {
        private const string PLAYER_CONTAINER_OBJECT_TAG = "PlayerResourceContainer";

        public string Name => "Resources";
        public string Description => null;
        public DifficultyAspectCategory Category => DifficultyAspectCategory.Resources;


        public int StartingCredits = 1000000;
        public int StartingResearch = 0;

        public void Apply()
        {
            var container = GameObject.FindGameObjectWithTag(PLAYER_CONTAINER_OBJECT_TAG).GetComponent<IResourceContainer>();
            container.ChangeResource(Resource.Credits, StartingCredits);
            container.ChangeResource(Resource.Research, StartingResearch);
        }
    }
}
