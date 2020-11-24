using Lomztein.BFA2.Player.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class MessageLogger
    {
        public static void Write(object content)
        {
            SendMessage(content.ToString());
        }

        public static void Error (object content)
        {
            SendMessage($"<color=red>{content}</color>");
        }

        public static void Warning (object content)
        {
            SendMessage($"<color=yellow>{content}</color>");
        }

        public static void Exception (Exception exc)
        {
            SendMessage($"<color=red>{exc.Message} - {exc.StackTrace}</color>");
        }

        private static void SendMessage (string content)
        {
            Message.Send(content, Message.Type.Minor);
        }
    }
}
