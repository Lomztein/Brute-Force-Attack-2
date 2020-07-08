using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.World
{
    public class Graph
    {
        private Node[] _nodes;
        private Edge[] _edges;

        public Node[] GetNodes() => _nodes;
        public Edge[] GetEdges() => _edges;
        public IGraphMap Map { get; private set; }

        public Graph(Node[] nodes, Edge[] edges, IGraphMap map)
        {
            _nodes = nodes;
            _edges = edges;
            Map = map;

            CacheEdges();
        }

        private void CacheEdges()
        {
            List<int>[] incoming = new List<int>[_nodes.Length];
            List<int>[] outgoing = new List<int>[_nodes.Length];

            for (int i = 0; i < _nodes.Length; i++)
            {
                incoming[i] = new List<int>();
                outgoing[i] = new List<int>();
            }

            for (int i = 0; i < _edges.Length; i++)
            {
                Edge edge = _edges[i];
                incoming[edge.To].Add(i);
                outgoing[edge.From].Add(i);
            }

            for (int i = 0; i < _nodes.Length; i++)
            {
                _nodes[i].SetIncomingEdges(incoming[i].ToArray());
                _nodes[i].SetOutgoingEdges(outgoing[i].ToArray());
            }
        }

        public Edge[] GetIncomingEdges(Node node)
            => node.GetIncomingEdges().Select(x => _edges[x]).ToArray();

        public Edge[] GetOutgoingEdges(Node node)
            => node.GetOutgoingEdges().Select(x => _edges[x]).ToArray();

        public class Node
        {
            public Vector3 Position { get; private set; }
            private int[] _incomingEdges;
            private int[] _outgoingEdges;

            public int[] GetIncomingEdges() => _incomingEdges;
            public int[] GetOutgoingEdges() => _outgoingEdges;

            public void SetIncomingEdges(int[] edges) => _incomingEdges = edges;
            public void SetOutgoingEdges(int[] edges) => _outgoingEdges = edges;

            public Node(Vector3 position)
            {
                Position = position;
            }
        }

        public class Edge
        {
            public int From { get; private set; }
            public int To { get; private set; }

            public Edge(int from, int to)
            {
                From = from;
                To = to;
            }
        }
    }
}