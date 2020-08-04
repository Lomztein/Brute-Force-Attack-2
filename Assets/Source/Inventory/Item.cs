using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Inventory
{
    public abstract class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Sprite Sprite { get; private set; }
    }
}
