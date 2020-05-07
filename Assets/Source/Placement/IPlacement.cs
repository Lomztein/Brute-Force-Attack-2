using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public interface IPlacement
    {
        bool ToPosition(Vector2 position, Quaternion rotation);

        bool Place();
        event Action OnPlaced;

        bool Cancel();
        event Action OnCancelled;

        bool Pickup(GameObject obj);
    }
}
