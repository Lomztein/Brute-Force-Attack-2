using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class SugiyamaGraphOrganizer : IEnumerable<SugiyamaGraphOrganizer.Node>
    {
        private List<Node> _nodes = new List<Node>();

        public Node AddNode () {
            Node node = new Node();
            _nodes.Add(node);
            return node;
        }

        public void AddEdge (Node from, Node to) {
            Edge edge = new Edge();
            to.ConnectTo(from, edge, true);
            from.ConnectTo(to, edge, false);
        }

        public IEnumerable<Node> GetRootNodes () => _nodes.Where(x => x.ParentEdges.Count() == 0);

        public void Organize() {
            // Implement of Sugiyama-style graph drawing, see https://en.wikipedia.org/wiki/Layered_graph_drawing

            // Step #1: Assign each node to a layer that is beneath any of its parent and above any child.
            var untravelled = new Queue<Node>(GetRootNodes());
            while (untravelled.Count != 0)
            {
                var node = untravelled.Dequeue();
                if (node.ParentEdges.Count() != 0)
                {
                    node.SetPosition(node.X, Mathf.Min (node.Y, node.ParentEdges.Min(x => x.ParentNode.Y) - 1));
                }
                foreach (var child in node.ChildEdges)
                {
                    untravelled.Enqueue(child.ChildNode);
                }
            }

            // Step #2: Create dummy nodes where an edge spans multiple layers.

        }

        public IEnumerator<Node> GetEnumerator()
        {
            return ((IEnumerable<Node>)_nodes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_nodes).GetEnumerator();
        }

        public class Node {

            public float X { get; private set; }
            public float Y { get; private set; }

            public void SetPosition (float x, float y) {
                X = x;
                Y = y;
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
