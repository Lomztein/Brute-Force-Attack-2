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
                if (node.ParentEdges.Count() == 0)
                {
                    SetLayer(node, 0, layers);
                }
                else
                {
                    int layer = Mathf.Max(node.Layer, node.ParentEdges.Max(x => x.ParentNode.Layer) + 1);
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

                    Node prevNode = pair.Value;
                    for (int i = pair.Value.Layer + 1; i < child.Layer; i++)
                    {
                        Node node = new Node("DUMMY");
                        Edge edge = new Edge();

                        prevNode.ConnectTo(node, edge, true);
                        node.ConnectTo(prevNode, edge, false);

                        prevNode = node;

                        SetLayer(node, i, layers);
                        dummyNodes.Add(node);
                    }

                    Edge final = new Edge();
                    prevNode.ConnectTo(child, final, true);
                    child.ConnectTo(prevNode, final, false);
                }
            }

            AssignCoordinates(layers);
            int iters = 10;

            // Step #3: Permutate nodes within single layers to reduce crossings between previous layer.
            for (int a = 0; a < iters; a++)
            {
                for (int l = layers.Count - 1; l >= 0; l--)
                {
                    var layer = layers.ElementAt(l);
                    for (int i = 0; i < layer.Value.Count; i++)
                    {
                        for (int j = 0; j < layer.Value.Count; j++)
                        {
                            AssignCoordinates(layers);

                            // Try swap, reverse if crossings are not reduced.
                            int intersections = CountIntersectionsBelow(l, layers);

                            Swap(layer.Value, i, j);
                            AssignCoordinates(layers);

                            int newIntersections = CountIntersectionsBelow(l, layers);

                            if (newIntersections >= intersections) // If there are now more or equal intersections..
                            {
                                Swap(layer.Value, i, j); // Reverse the swap.
                            }

                            AssignCoordinates(layers);
                        }
                    }
                }
            }

            // Step #4: Assign coordinates.
            AssignCoordinateBasedOfChildren(layers);

            foreach (var layer in layers)
            {
                if (layers.ContainsKey(layer.Key - 1))
                {
                    var l1 = layer.Value;
                    var l2 = layers[layer.Key - 1];

                    int intersections = CountIntersections(l1, l2);
                    Debug.Log($"There are {intersections} intersections between layer {layer.Key} and {layer.Key - 1}");
                }
            }
        }

        private int CountIntersectionsBelow (int layer, Dictionary<int, List<Node>> layers)
        {
            int intersections = 0;
            if (layer < layers.Count - 1)
            {
                intersections += CountIntersections(layers[layer], layers[layer + 1]);
            }
            if (layer > 1)
            {
                intersections += CountIntersections(layers[layer], layers[layer - 1]);
            }
            return intersections;
        }

        private static void AssignCoordinates(Dictionary<int, List<Node>> layers) 
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

        private static void AssignCoordinateBasedOfChildren(Dictionary<int, List<Node>> layers)
        {
            foreach (var pair in layers)
            {
                for (int j = 0; j < pair.Value.Count; j++)
                {
                    Node node = pair.Value[j];

                    float mean = 0f;
                    foreach (var parent in node.Parents)
                    {
                        mean += parent.X;
                    }
                    mean /= Mathf.Max (1, node.Parents.Count());

                    node.X = (mean + (j - (pair.Value.Count / 2f)));
                }
            }
        } 

        // Yoinked from https://stackoverflow.com/questions/2094239/swap-two-items-in-listt because I was lazy.
        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        private static int CountIntersections (List<Node> layer1, List<Node> layer2)
        {
            int intersections = 0;
            foreach (var pair1 in GetPairsBetweenLayers(layer1, layer2))
            {
                foreach (var pair2 in GetPairsBetweenLayers(layer1, layer2))
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

        private static IEnumerable<Tuple<Node, Node>> GetPairsBetweenLayers (List<Node> layer1, List<Node> layer2)
        {
            int count = 0;
            foreach (Node n in layer1)
            {
                foreach (var parent in n.Parents)
                {
                    if (layer2.Contains(parent))
                    {
                        count++;
                        yield return new Tuple<Node, Node>(n, parent);
                    }
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
        }
    }
}
