using Lomztein.BFA2.Serialization.Models.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models.GameObject
{
    public interface IGameObjectModel : ISerializable
    {
        string Name { get; }
        string Tag { get; }
        int Layer { get; }
        bool Static { get; }

        IComponentModel[] GetComponentModels();
        IGameObjectModel[] GetChildren();
    }
}
