using Lomztein.BFA2.Mutators;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public class CompositeMutator : Mutator, IHasProperties
    {
        [ModelProperty]
        public Mutator[] Children;

        public void AddPropertiesTo(PropertyMenu menu)
        {
            foreach (Mutator child in Children)
            {
                if (child is IHasProperties withProperties)
                {
                    withProperties.AddPropertiesTo(menu);
                }
            }
        }

        public override void Start()
        {
            foreach (Mutator child in Children)
            {
                child.Start();
            }
        }
    }
}
