using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World.Tiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.World.Pathfinding
{
    // This implementation is quite the travesty, will likely reimplement a better version later.
    // I did it all from hazy memory based on a python script I worked with several months ago.
    // Additionally some changed behaviour required added functionality that increased complexity.
    public class AStarPathfinder : MonoBehaviour, IPathfinder
    {
        private readonly LooseDependancy<MapController> _map = new LooseDependancy<MapController>();
        private readonly LooseDependancy<TileController> _tiles = new LooseDependancy<TileController>();

        private TileType[] _blockingTypes = new TileType[]
        {
            TileType.BlockingWall,
            TileType.PlayerWall
        };

        public Vector3[] Search(Vector3 start, params Vector3[] end)
        {
            if (!_map.Exists || !_tiles.Exists)
            {
                throw new InvalidOperationException("Map- and TileControllers are missing, they need to be added into the scene and given their respective tags.");
            }

            if (!_map.Dependancy.InInsideMap(start) || end.Any(x => !_map.Dependancy.InInsideMap(x)))
            {
                return null;
            }

            Graph graph = _map.Dependancy.MapGraph;
            TileData tiles = _tiles.Dependancy.TileData;

            Graph.Node startingNode = graph.Map.GetNode(start);
            IEnumerable<Graph.Node> endingNodes = end.Select (x => graph.Map.GetNode(x));

            (Node starting, IEnumerable<Node> nodes, IEnumerable<Node> ending) = ConvertNodes(tiles, graph, startingNode, graph.GetNodes(), endingNodes);

            List<Node> fringe = new List<Node>()
            {
                { starting }
            };

            Node endingNode = null;
            while (endingNode == null && fringe.Count != 0)
            {
                endingNode = ExploreFringe(fringe, ending);
            }
            if (endingNode == null)
            {
                return null;
            }
            else
            {
                return TraverseNodes(endingNode).Select(x => MapUtils.TileToWorldCoords(new Vector2Int(Mathf.RoundToInt(x.x), Mathf.RoundToInt(x.y)), tiles.Width, tiles.Height)).ToArray();
            }
        }

        private Node ExploreFringe (List<Node> fringe, IEnumerable<Node> endNodes)
        {
            Node node = GetLowestHeuristic(fringe, endNodes);
            node.Explored = true;

            fringe.Remove(node);
            return Expand(fringe, node);
        }

        private Node Expand (List<Node> fringe, Node node)
        {
            foreach (Node connected in node.Connected)
            {
                if (connected.Explored)
                {
                    continue;
                }

                connected.Parent = node;
                if (connected.IsEnd)
                {
                    return connected;
                }

                connected.Explored = true;
                fringe.Add(connected);
            }

            return null;
        }

        private Node GetLowestHeuristic (IEnumerable<Node> nodes, IEnumerable<Node> endNodes)
        {
            float lowestValue = float.MaxValue;
            Node lowest = null;

            foreach (Node node in nodes)
            {
                float value = node.GetCost () + Heuristic(node, endNodes);
                if (value < lowestValue)
                {
                    lowestValue = value;
                    lowest = node;
                }
            }

            return lowest;
        }

        private (Node starting, IEnumerable<Node> nodes, IEnumerable<Node> endingNodes) ConvertNodes (TileData tiles, Graph graph, Graph.Node starting, IEnumerable<Graph.Node> nodes, IEnumerable<Graph.Node> endNodes)
        {
            Node[] ns = nodes.Select(x => new Node()
            {
                Position = x.Position,
                Cost = _blockingTypes.Any(y => tiles.GetTile(Mathf.RoundToInt(x.Position.x), Mathf.RoundToInt(x.Position.y)).WallType == y.Name) ? 1000 : 1,
                IsEnd = endNodes.Contains(x),
                Original = x
            }).ToArray();

            Node start = null;
            List<Node> endingNodes = new List<Node>();
            foreach (Node node in ns)
            {
                if (node.Original == starting)
                {
                    start = node;
                }

                node.Connected = node.Original.GetOutgoingEdges().Select(x => ns[graph.GetEdges()[x].To]);

                if (node.IsEnd)
                {
                    endingNodes.Add(node);
                }
            }

            return (start, ns, endingNodes);
        }

        private Node GetClosest (Vector3 from, IEnumerable<Node> endNodes)
        {

            float closestValue = float.MaxValue;
            Node closest = null;

            foreach (Node node in endNodes)
            {
                float value = Vector3.SqrMagnitude(from - node.Position);
                if (value < closestValue)
                {
                    closest = node;
                    closestValue = value;
                }
            }

            return closest;
        }

        private float Heuristic (Node node, IEnumerable<Node> endingNodes)
        {
            return Vector3.SqrMagnitude(GetClosest(node.Position, endingNodes).Position - node.Position);
        }

        private IEnumerable<Vector3> TraverseNodes (Node endingNode)
        {
            List<Vector3> waypoints = new List<Vector3>();
            Node current = endingNode;
            waypoints.Add(endingNode.Position);
            while (current.Parent != null)
            {
                current = current.Parent;
                waypoints.Add(current.Position);
            }
            waypoints.Reverse();
            return waypoints;
        }

        private class Node
        {
            public Vector3 Position;
            public bool IsEnd;
            public Node Parent;
            public IEnumerable<Node> Connected;
            public Graph.Node Original;

            public bool Explored;
            public float Cost;
            public float GetCost() => Cost + (Parent?.GetCost()).GetValueOrDefault();
        }

        private class Comparer : IComparer
        {
            public int Compare(object x, object y)
            {
                return Mathf.RoundToInt((float)x - (float)y);
            }
        }

    }
}