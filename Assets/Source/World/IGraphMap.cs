using UnityEngine;

namespace Lomztein.BFA2.World
{
    public interface IGraphMap
    {
        Graph.Node GetNode(Vector3 position);
    }
}