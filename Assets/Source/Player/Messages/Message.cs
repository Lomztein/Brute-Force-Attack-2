using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Messages
{
    public class Message
    {
        public string Content { get; private set; }

        private const float DEFAULT_TIME = 10;
        private readonly float _time;

        public enum Type { Major, Minor }

        public Message (string content) : this(content, DEFAULT_TIME)
        {
        }

        public Message (string content, float time)
        {
            Content = content;
            _time = time;
        }

        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(_time);
        }

        public static void Send(string content, Type type)
            => Send(new Message(content), type);

        public static void Send(string content, float time, Type type)
            => Send(new Message(content, time), type);

        public static void Send(Message message, Type type)
        {
            switch (type)
            {
                case Type.Major:
                    SendMajor(message);
                    break;

                case Type.Minor:
                    SendMinor(message);
                    break;
            }

            // what the hell am I even doing
        }

        private static void SendMajor (Message message)
        {
            // Perhaps this check should be on a static function on the controller.
            // I do not feel that checking an manually adding the message should be this classes responsibility.
            if (MajorMessageController.Instance)
            {
                MajorMessageController.Instance.Enqueue(message);
            }
        }

        private static void SendMinor (Message message)
        {
            if (MinorMessageController.Instance)
            {
                MinorMessageController.Instance.AddMessage(message);
            }
        }
    }
}
