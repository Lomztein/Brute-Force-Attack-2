using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Component;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public class LineRendererSerializer : IEngineComponentSerializer
    {
        public Type Type => typeof(LineRenderer);

        public void Deserialize(IComponentModel model, GameObject target)
        {
            throw new NotImplementedException();
        }

        public IComponentModel Serialize(Component source)
        {
            throw new NotImplementedException();
        }
    }
}
