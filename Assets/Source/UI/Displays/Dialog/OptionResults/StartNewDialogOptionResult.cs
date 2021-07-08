using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Displays.Dialog.OptionResults
{
    [System.Serializable]
    public class StartNewDialogOptionResult : IDialogOptionResult
    {
        [ModelAssetReference]
        public DialogTree Tree;

        public void OnSelected()
        {
            DialogDisplay.DisplayDialog(Tree);
        }
    }
}
