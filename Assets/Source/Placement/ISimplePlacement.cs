using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    // TODO: Rename to IStructurePlacement, 
    public interface ISimplePlacement : IPlacement
    {
        bool ToPosition(Vector2 position);
        bool ToRotation(Quaternion rotation);
        bool Flip();

        bool Place();
        event Action<GameObject> OnPlaced;

        bool Pickup(GameObject obj);
    }
}
