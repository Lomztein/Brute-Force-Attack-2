using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Research.Rewards
{
    public class MessageReward : CompletionReward
    {
        public override string Description => "Recieve a message.";

        [ModelProperty]
        public string Content = "Message Content";
        [ModelProperty]
        public float Time = 10;
        [ModelProperty]
        public Message.Type Type = Message.Type.Major;

        public override void ApplyReward()
        {
            Message.Send(Content, Time, Type);
        }
    }
}
