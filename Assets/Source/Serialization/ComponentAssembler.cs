using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization
{
    public class ComponentAssembler
    {
        public Component Assemble (IComponentModel model)
        {
            throw new NotImplementedException();
        }

        public IComponentModel Dissasemble(Component component) => ComponentModel.Create(component);
    }
}
