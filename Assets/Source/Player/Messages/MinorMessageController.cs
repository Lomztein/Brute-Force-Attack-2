using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Player.Messages
{
    public class MinorMessageController : MonoBehaviour
    {
        public static MinorMessageController Instance;
        public Transform MessageParent;
        public GameObject MessagePrefab;

        public void Awake()
        {
            Instance = this;
        }

        public void AddMessage (Message message)
        {
            StartCoroutine(Display(message));
        }

        private IEnumerator Display (Message message)
        {
            GameObject messageObj = Instantiate(MessagePrefab, MessageParent);
            messageObj.GetComponent<Text>().text = message.Content;
            yield return message.Wait();
            Destroy(messageObj);
        }
    }
}
