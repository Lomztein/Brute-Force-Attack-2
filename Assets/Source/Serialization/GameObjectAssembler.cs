using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectAssembler
{
    public GameObject Assemble(IGameObjectModel model)
    {
        return new GameObject(model.Name);
    }

    public IGameObjectModel Dissassemble(GameObject gameObject) => GameObjectModel.Create(gameObject);
}
