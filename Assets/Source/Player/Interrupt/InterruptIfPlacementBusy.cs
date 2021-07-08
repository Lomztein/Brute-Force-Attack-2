using Lomztein.BFA2.Placement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Interrupt
{
    public class InterruptIfPlacementBusy : MonoBehaviour, IInterrupt // There's gotta be a better way then this. TODO: Find a better way.
    {
        public bool IsInterrupted() => PlacementController.Instance.Busy;
    }
}
