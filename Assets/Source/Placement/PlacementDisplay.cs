using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Placement
{
    public class PlacementDisplay : MonoBehaviour
    {
        public Text Text;
        public SimplePlacementBehaviour PlacementController;

        private void Update()
        {
            Text.text = PlacementController.CurrentToString();
        }
    }
}
