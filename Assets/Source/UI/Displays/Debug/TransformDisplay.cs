using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays.Debug
{
    public class TransformDisplay : MonoBehaviour
    {
        public Transform Target;
        public Text Text;

        public void Update()
        {
            Text.text = Target.gameObject.name + "\n" + Target.position + "\n" + Target.rotation + "\n" + Target.localScale;
        }
    }
}
