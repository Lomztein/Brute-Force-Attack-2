using Lomztein.BFA2.Serialization;
using UnityEngine;

public interface IGameObjectAssembler
{
    GameObject Assemble(IGameObjectModel model);
    IGameObjectModel Dissassemble(GameObject gameObject);
}