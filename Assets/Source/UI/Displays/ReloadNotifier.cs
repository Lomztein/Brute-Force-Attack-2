using Lomztein.BFA2.ContentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays
{
    public class ReloadNotifier : MonoBehaviour
    {
        public GameObject ContentNotifier;
        public GameObject GameNotifier;

        void Update()
        {
            ContentNotifier.SetActive(ContentManager.NeedsContentReload);
            GameNotifier.SetActive(ContentManager.NeedsApplicationReload);
        }
    }
}
