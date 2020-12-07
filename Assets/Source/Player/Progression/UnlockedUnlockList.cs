using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression
{
    public class UnlockedUnlockList : MonoBehaviour, IUnlockList
    {
        public event Action<string, bool> OnUnlockChange;

        public void Add(string identifier, bool unlocked)
        {
        }

        public bool IsUnlocked(string identifier)
        {
            return true;
        }

        public void SetUnlocked(string identifier, bool value)
        {
        }
    }
}
