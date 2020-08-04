using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression
{
    public class UnlockLists : MonoBehaviour
    {
        public static UnlockLists Instance;
        private IUnlockList[] _unlockLists;

        public static IUnlockList Get(string name) => Instance._unlockLists.FirstOrDefault(x => x.Name == name);

        private void Awake()
        {
            Instance = this;
            _unlockLists = GetComponents<IUnlockList>();
        }
    }
}
