using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class PlacementController : MonoBehaviour
    {
        public static PlacementController Instance;

        [SerializeField] private IPlacementBehaviour[] _behaviours;

        public bool Busy => _behaviours.Any(x => x.Busy);

        public void Awake()
        {
            Instance = this;
            _behaviours = GetComponents<IPlacementBehaviour>();
        }

        public void TakePlacement (IPlacement placement)
        {
            CancelAll();
            GetBehaviour(placement.GetType()).TakePlacement(placement);
        }

        public void CancelAll ()
        {
            foreach (var behaviour in _behaviours)
            {
                behaviour.Cancel();
            }
        }

        private IPlacementBehaviour GetBehaviour(Type placementType) => _behaviours.First(x => x.CanHandleType(placementType));
    }
}
