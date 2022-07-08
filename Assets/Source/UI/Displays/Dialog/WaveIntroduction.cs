using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.UI.Displays.Dialog
{
    [CreateAssetMenu(menuName = "BFA2/Dialog/WaveIntroduction", fileName = "New WaveIntroduction")]
    public class WaveIntroduction : ScriptableObject
    {
        [ModelAssetReference]
        public Character Character;
        [ModelProperty]
        public string Expression;
        [TextArea, ModelProperty]
        public string Text;
        [ModelProperty, SerializeReference, SR]
        private IWaveIntroductionPredicate Predicate;

        public bool ShouldShow(int forWave)
        {
            return Predicate.ShouldShow(forWave);
        }

        public DialogNode GenerateDialogNode()
        {
            DialogNode node = new DialogNode();
            node.Text = Text;
            node.Expression = Expression;
            node.Character = Character;

            DialogNode.Option option = new DialogNode.Option();
            option.Text = "Understood";

            node.Options = new DialogNode.Option[]
            {
                option
            };

            return node;
        }
    }
}
