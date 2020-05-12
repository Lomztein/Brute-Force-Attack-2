using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public interface IEngineComponentSerializer
    {
        Type Type { get; }

        IComponentModel Serialize(Component source);

        void Deserialize(IComponentModel model, GameObject target);
    }
}
