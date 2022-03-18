using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Utilities
{
    public class LayeredGraphOrganizer : IEnumerable<KeyValuePair<string, LayeredGraphOrganizer.Node>>
    {
        private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();

        public Node AddNode(string key) {
            Node node = new Node(key);
            _nodes.Add(key, node);
            return node;
        }

        public Node GetNode(string key) => _nodes[key];

        public void RemoveNode(string key)
        {
            Node node = _nodes[key];
            DisconnectNode(node);
        }

        private void DisconnectNode (Node node)
        {
            foreach (var parent in node.Parents)
            {
                parent.DisconnectNode(node, true);
            }

            foreach (var child in node.Children)
            {
                child.DisconnectNode(node, false);
            }

            DisconnectEdges(node, node.ParentEdges, false);
            DisconnectEdges(node, node.ChildEdges, true);

        }

        private void DisconnectEdges(Node node, IEnumerable<Edge> edges, bool child)
        {
            var copy = edges.ToList();
            foreach (var edge in copy)
            {
                node.DisconnectEdge(edge, child);
            }
        }

        public void AddEdge(Node from, Node to) {
            Edge edge = new Edge();
            to.ConnectTo(from, edge, false);
            from.ConnectTo(to, edge, true);
        }

        public IEnumerable<Node> GetRootNodes() => _nodes.Select(x => x.Value).Where(x => x.ParentEdges.Count() == 0);

        public void Organize() {
            // Implement of Sugiyama-style graph drawing, see https://en.wikipedia.org/wiki/Layered_graph_drawing

            var layers = new Dictionary<int, List<Node>>();
            foreach (var pair in _nodes)
            {
                SetLayer(pair.Value, 0, layers);
            }

            // Step #1: Assign each node to a layer that is beneath any of its parent and above any child.
            var untravelled = new Queue<Node>(GetRootNodes());
            while (untravelled.Count != 0)
            {
                var node = untravelled.Dequeue();
                if (node.Parents.Count() == 0)
                {
                    SetLayer(node, 0, layers);
                }
                else
                {
                    int layer = Mathf.Max(node.Layer, node.Parents.Max(x => x.Layer) + 1);
                    SetLayer(node, layer, layers);
                }
                foreach (var child in node.ChildEdges)
                {
                    untravelled.Enqueue(child.ChildNode);
                }
            }

            var dummyNodes = new List<Node>();
            // Step #2: Create dummy nodes where an edge spans multiple layers.
            foreach (var pair in _nodes)
            {
                var children = pair.Value.Children.ToArray();
                foreach (var child in children)
                {
                    if (child.Layer <= pair.Value.Layer + 1)
                        break;

                    pair.Value.DisconnectNode(child, true);
                    child.DisconnectNode(pair.Value, false);

                    Node prevNode = pair.Value;
                    for (int index = pair.Value.Layer + 1; index < child.Layer; index++)
                    {
                        Node node = new Node("DUMMY");
                        Edge edge = new Edge();

                        prevNode.ConnectTo(node, edge, true);
                        node.ConnectTo(prevNode, edge, false);

                        prevNode = node;

                        SetLayer(node, index, layers);
                        dummyNodes.Add(node);
                    }

                    Edge final = new Edge();
                    prevNode.ConnectTo(child, final, true);
                    child.ConnectTo(prevNode, final, false);
                }
            }

            AssignCoordinatesBasedOnLayers(layers);

            // Step #3: Permutate nodes within single layers to reduce crossings between previous layer.
            int iters = 24;
            int n = layers.Count;
            
            for (int i = 0; i < iters; i++)
            {
                bool improved = false;
                for (int l = n - 1; l > 1; l--)
                {
                    if (ReduceCrossings(layers[l], layers[l - 1]))
                        improved = true;
                }
                for (int l = 0; l < n - 1; l++)
                {
                    if (ReduceCrossings(layers[l], layers[l + 1]))
                        improved = true;
                }
                if (improved == false)
                    break;
            }

            // Step #4: Assign coordinates.
            AssignCoordinateBasedOnNeightbors(layers);

            // Step #5: Remove dummies
            foreach (var node in dummyNodes)
            {
                foreach (var child in node.Children)
                {
                    foreach (var parent in node.Parents)
                    {
                        Edge edge = new Edge();
                        child.ConnectTo(parent, edge, false);
                        parent.ConnectTo(child, edge, true);
                    }
                }
                DisconnectNode(node);
            }
        }

        private bool ReduceCrossings (List<Node> l1, List<Node> l2)
        {
            bool improved = false;
            int length = l2.Count;

            for (int n1 = 0; n1 < length; n1++)
            {
                for (int n2 = 0; n2 < length; n2++)
                {
                    if (n1 == n2) continue;

                    int crossings = CountIntersections(l1, l2);
                    SwapNodes(l2, n1, n2);
                    int newCrossings = CountIntersections(l1, l2);

                    if (newCrossings >= crossings)
                    {
                        SwapNodes(l2, n1, n2);
                    }
                    else
                    {
                        improved = true;
                    }
                }
            }

            return improved;
        }

        private static void AssignCoordinatesBasedOnLayers(Dictionary<int, List<Node>> layers) 
        {
            for (int l = layers.Count - 1; l >= 0; l--)
            {
                var pair = layers.ElementAt(l);
                for (int j = 0; j < pair.Value.Count; j++)
                {
                    Node node = pair.Value[j];
                    node.SetPosition(j, pair.Key);
                }
            }

        }

        private static void AssignCoordinateBasedOnNeightbors(Dictionary<int, List<Node>> layers)
        {
            int iters = 24;
            for (int i = 0; i < iters; i++)
            {
                int n = layers.Count;
                for (int l = 0; l < n; l++)
                {
                    BarymetricSweepLayer(layers[l], true);
                }
                for (int l = n - 1; l >= 0; l--)
                {
                    BarymetricSweepLayer(layers[l], false);
                }
            }
        }

        // Code stolen from here https://github.com/fluffy-mods/ResearchTree/blob/b88a9d8820ebecfffdca179dbfcfd6736b55e817/Source/Graph/Tree.cs#L830
        private static void BarymetricSweepLayer (List<Node> layer, bool parents)
        {
            var means = layer
                       .ToDictionary(n => n, n => GetMeanCoordinate(parents ? n.Parents : n.Children))
                       .OrderBy(n => n.Value);

            // create groups of nodes at similar means
            var cur = float.MinValue;
            var groups = new Dictionary<float, List<Node>>();
            float epsilon = 0.01f;
            foreach (var mean in means)
            {
                if (Math.Abs(mean.Value - cur) > epsilon)
                {
                    cur = mean.Value;
                    groups[cur] = new List<Node>();
                }

                groups[cur].Add(mean.Key);
            }

            // position nodes as close to their desired mean as possible
            var X = 1f;
            foreach (var group in groups)
            {
                var mean = group.Key;
                var N = group.Value.Count;
                X = Mathf.Max(X, mean - (N - 1f) / 2f);

                foreach (var node in group.Value)
                    node.X = X++;
            }
        }

        private static float GetMeanCoordinate(IEnumerable<Node> nodes)
        {
            if (!nodes.Any())
            {
                return 0f;
            }
            float min = nodes.Min(x => x.X);
            float max = nodes.Max(x => x.X);
            return Mathf.Lerp(min, max, 0.5f);
        }
        
        private static float[,] ToAdjacencyMatrix (Node[] nodes)
        {
            int count = nodes.Length;

            var result = new float[count, count];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    float value = IsConnected(nodes[i], nodes[j]) ? 1 : 0;
                    result[i, j] = value;
                }
            }
            return result;
        }

        private static bool IsConnected (Node n1, Node n2)
            => n1.Children.Contains(n2) || n1.Parents.Contains(n2);

        private static float IsConnectedToFloat(Node n1, Node n2)
            => IsConnected(n1, n2) ? 1f : 0f;

        // Yoinked from https://stackoverflow.com/questions/2094239/swap-two-items-in-listt because I was lazy.
        private static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            (list[indexB], list[indexA]) = (list[indexA], list[indexB]);
        }

        private static void SwapNodes(List<Node> nodes, int n1, int n2)
        {
            float temp = nodes[n2].X;
            nodes[n2].X = nodes[n1].X;
            nodes[n1].X = temp;

            Swap(nodes, n1, n2);
        }

        private static int CountIntersections (List<Node> layer1, List<Node> layer2)
        {
            int intersections = 0;
            foreach (var pair1 in GetPairsBetweenLayers(layer1, layer2, true))
            {
                foreach (var pair2 in GetPairsBetweenLayers(layer1, layer2, true))
                {
                    if (pair1.Item1 == pair2.Item1 && pair1.Item2 == pair2.Item2) continue; // Skip when they're the same.
                    if (LineIntersection.LinesIntersect(
                        new Vector2(pair1.Item1.X, pair1.Item1.Layer),
                        new Vector2(pair1.Item2.X, pair1.Item2.Layer),
                        new Vector2(pair2.Item1.X, pair2.Item1.Layer),
                        new Vector2(pair2.Item2.X, pair2.Item2.Layer), 0.1f)) {
                        intersections++;
                    }
                }
            }

            return intersections / 2; // Each intersection will be counted twice.
        }

        private static int CountIntersections (Dictionary<int, List<Node>> layers)
        {
            int intersections = 0;
            var nodes = layers.SelectMany(x => x.Value);
            foreach (var pair1 in GetAllPairs(nodes, true))
            {
                foreach (var pair2 in GetAllPairs(nodes, false))
                {
                    if (pair1.Item1 == pair2.Item1 && pair1.Item2 == pair2.Item2) continue; // Skip when they're the same.
                    if (LineIntersection.LinesIntersect(
                        new Vector2(pair1.Item1.X, pair1.Item1.Layer),
                        new Vector2(pair1.Item2.X, pair1.Item2.Layer),
                        new Vector2(pair2.Item1.X, pair2.Item1.Layer),
                        new Vector2(pair2.Item2.X, pair2.Item2.Layer), 0.1f))
                    {
                        intersections++;
                    }
                }
            }

            return intersections / 2; // Each intersection will be counted twice.
        }

        private static IEnumerable<Tuple<Node, Node>> GetPairsBetweenLayers (List<Node> layer1, List<Node> layer2, bool children)
        {
            foreach (Node n in layer1)
            {
                var list = children ? n.Children : n.Parents;
                foreach (var neighbor in list)
                {
                    if (layer2.Contains(neighbor))
                    {
                        yield return new Tuple<Node, Node>(n, neighbor);
                    }
                }
            }
        }

        private static IEnumerable<Tuple<Node, Node>> GetAllPairs (IEnumerable<Node> nodes, bool children)
        {
            foreach (Node node in nodes)
            {
                var list = children ? node.Children : node.Parents;
                foreach (var neighbor in list)
                {
                    yield return new Tuple<Node, Node>(node, neighbor);
                }
            }
        }

        private static void SetLayer (Node node, int layer, Dictionary<int, List<Node>> layers)
        {
            int curLayer = GetLayer(node);
            if (layers.ContainsKey(curLayer))
            {
                layers[curLayer].Remove(node);
            }

            if (!layers.ContainsKey(layer))
            {
                layers.Add(layer, new List<Node>());
            }

            node.Layer = layer;
            layers[layer].Add(node);
        }

        private static int GetLayer(Node node)
            => node.Layer; // I dunno feels right.

        public IEnumerator<KeyValuePair<string, Node>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, Node>>)_nodes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_nodes).GetEnumerator();
        }

        public class Node {

            public string Key { get; private set; }
            public float X { get; set; } = 0;
            public int Layer { get; set; } = 0;

            public Node (string key)
            {
                Key = key;
            }

            public void SetPosition(float x, int layer)
            {
                X = x;
                Layer = layer;
            }

            public void ConnectTo (Node other, Edge edge, bool child) {
                if (child) {
                    edge.Connect(other, this);
                    _childEdges.Add(edge);
                }else{
                    edge.Connect(this, other);
                    _parentEdges.Add(edge);
                }
            }

            public void DisconnectEdge(Edge edge, bool child)
            {
                if (child) _childEdges.Remove(edge);
                else _parentEdges.Remove(edge);
            }

            public void DisconnectNode (Node node, bool child)
            {
                DisconnectEdge(GetEdge(node, child), child);
            }

            public Edge GetEdge(Node node, bool child)
            {
                if (child) return _childEdges.FirstOrDefault(x => x.ChildNode == node);
                else return _parentEdges.FirstOrDefault(x => x.ParentNode == node);
            }

            private List<Edge> _childEdges = new List<Edge>();
            private List<Edge> _parentEdges = new List<Edge>();

            public IEnumerable<Edge> ChildEdges => _childEdges;
            public IEnumerable<Edge> ParentEdges => _parentEdges;

            public IEnumerable<Node> Children => _childEdges.Select(x => x.ChildNode);
            public IEnumerable<Node> Parents => _parentEdges.Select(x => x.ParentNode);

            public override string ToString()
            {
                return $"{Key} ({Layer}, {X})";
            }
        }

        public class Edge {

            public void Connect (Node child, Node parent) {
                ChildNode = child;
                ParentNode = parent;
            }

            public Node ChildNode { get; private set; }
            public Node ParentNode { get; private set; }

            public void Disconnect ()
            {
                ChildNode = null;
                ParentNode = null;
            }
        }
    }
}
