using Lomztein.BFA2.UI.Displays.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Interrupt
{
    public class InterruptIfDialogOpen : MonoBehaviour, IInterrupt
    {
        public bool IsInterrupted() => DialogDisplay.Instance.IsOpen;
    }
}
