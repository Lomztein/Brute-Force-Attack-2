using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content
{
    public class Content : MonoBehaviour
    {
        private IContentManager _manager;
        private static Content _instance;

        private void Awake()
        {
            _instance = this;
            _manager = GetComponent<IContentManager>();
        }

        public static object Get(string path, Type type) => _instance._manager.GetContent(path, type);

        public static object[] GetAll(string path, Type type) => _instance._manager.GetAllContent(path, type);
    }
}
