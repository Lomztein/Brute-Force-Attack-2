using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Displays.Dialog.OptionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.UI.Displays.Dialog
{
    [Serializable]
    public class DialogNode
    {
        [ModelAssetReference]
        public Character Character;
        [ModelProperty]
        public string Expression;
        [ModelProperty, TextArea]
        public string Text;
        [ModelProperty]
        public Option[] Options;

        public Texture2D GetAvatar() => Character.GetAvatar(Expression);

        [Serializable]
        public class Option
        {
            [ModelProperty]
            public string Text;
            [ModelProperty, SerializeReference, SR]
            public IDialogOptionResult Result;
            public bool HasResult => Result != null;

            public void OnSelected() => Result.OnSelected();
        }
    }
}
