using Lomztein.BFA2.Player.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2
{
    public static class Log
    {
        public static void Write(object content)
        {
            SendMessage(content);
            Debug.Log(content);
        }

        public static void Error (object content)
        {
            Debug.LogWarning(content);
            SendMessage($"<color=red>{content}</color>");
        }

        public static void Warning (object content)
        {
            Debug.LogWarning(content);
            SendMessage($"<color=yellow>{content}</yellow>");
        }

        public static void Exception (Exception exc)
        {
            Log.Error(exc);
            SendMessage($"<color=red>{exc.Message} - {exc.StackTrace}</color>");
        }

        private static void SendMessage (object content)
        {
            if (Application.isEditor || Debug.isDebugBuild)
            {
                Message.Send(content.ToString(), Message.Type.Minor);
            }
        }
    }
}
