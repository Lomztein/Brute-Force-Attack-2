using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Dialog
{
    [CreateAssetMenu(fileName = "New DialogTree", menuName = "BFA2/Dialog/DialogTree")]
    public class DialogTree : ScriptableObject
    {
        [ModelProperty]
        public DialogNode Root;
    }
}
