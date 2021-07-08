using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Displays.Dialog.OptionResults
{
    [Serializable]
    public class NextNodeDialogOptionResult : IDialogOptionResult
    {
        [ModelProperty]
        public DialogNode Next;

        public void OnSelected()
        {
            DialogDisplay.DisplayDialogNode(Next);
        }
    }
}
