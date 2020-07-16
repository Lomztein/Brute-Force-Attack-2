using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public abstract class PlacementBehaviourBase<T> : MonoBehaviour, IPlacementBehaviour where T : IPlacement
    {
        public abstract bool Busy { get; }

        public abstract void Cancel();
        public bool CanHandleType(Type placementType) => typeof(T).IsAssignableFrom(placementType);

        public void TakePlacement(IPlacement placement)
        {
            TakePlacement((T)placement);
        }

        public abstract void TakePlacement(T placement);
    }
}
