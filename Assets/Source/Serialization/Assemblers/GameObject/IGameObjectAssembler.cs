using Lomztein.BFA2.Serialization.Models.GameObject;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public interface IGameObjectAssembler
    {
        GameObject Assemble(IGameObjectModel model);
        IGameObjectModel Dissassemble(GameObject gameObject);
    }
}