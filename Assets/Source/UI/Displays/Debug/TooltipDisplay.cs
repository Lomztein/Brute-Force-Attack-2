using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays.Debug
{
    public class TooltipDisplay : MonoBehaviour
    {
        public TooltipController Controller;
        public Text Text;

        public void Update()
        {
            try
            {
                Text.text = "Title: " + Controller.TooltipTitle.text +
                    "\nDescription: " + Controller.TooltipDescription.text +
                    "\nFooter: " + Controller.TooltipFooter.text +
                    "\nOffset: " + Controller.Offset.ToString() +
                    "\nUpdaters: " + Controller.Updaters.Length + "\n\t" + string.Join("\n\t", Controller.Updaters.Select(x => x.name));
            }catch (Exception e)
            {
                Text.text = e.Message + "\n" + e.StackTrace;
            }
        }
    }
}
