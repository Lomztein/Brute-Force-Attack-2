using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Interrupt
{
    public interface IInterrupt // I don't think there's any real need for a common ancestor for these but whatever.
    {
        public bool IsInterrupted();
    }
}
