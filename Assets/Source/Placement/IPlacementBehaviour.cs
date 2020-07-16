using System;

namespace Lomztein.BFA2.Placement
{
    public interface IPlacementBehaviour
    {
        bool Busy { get; }

        void Cancel();

        void TakePlacement(IPlacement placement);

        bool CanHandleType(Type placementType);
    }
}