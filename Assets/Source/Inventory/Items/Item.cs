using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Inventory.Items
{
    public abstract class Item : MonoBehaviour
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public Sprite Sprite;

        public void OnAssembled()
        {
            Init();
        }

        private void Start()
        {
            Init();
        }

        protected abstract void Init();

        public void Destroy ()
        {
            Destroy(gameObject);
        }
    }
}
