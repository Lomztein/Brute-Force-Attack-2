using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class SelfOrganizingGraph : IEnumerable<SelfOrganizingGraph.Node>
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

        public IEnumerable<Node> GetRootNodes () => _nodes.Where(x => x.IncomingEdges.Count() == 0);
        public IEnumerable<Node> GetLeafNodes() => _nodes.Where(x => x.OutgoingEdges.Count() == 0);

        public void Organize() {
            // Goal #1: No two nodes may share both X and Y values.
            // Goal #2: All children must have a lower Y-value than their parents.
            // Goal #3: All parents must have a higher Y-value than their children.
            // Goal #4: Min and max length edges between a parent and their children should be as close as possible.

            // Sort all according to heritage by traversing from root nodes and downwards.
            OrganizeNode(GetRootNodes().First());
        }

        private Rect OrganizeNode (Node node)
        {
            Rect rect = new Rect(node.X - node.Width / 2f, node.Y - node.Height / 2f, node.Width, node.Height);

            float cumWidth = 0f;
            foreach (var child in node.OutgoingEdges)
            {
                Node childNode = child.InNode;
                Rect childRect = OrganizeNode(child.InNode);
                childNode.SetPosition(cumWidth, Mathf.Min(childNode.Y, node.Y - 1));
                cumWidth += childRect.width;

                rect = rect.Encapsulate(childRect);
            }

            foreach (var child in node.OutgoingEdges)
            {
                Node childNode = child.InNode;
                childNode.SetPosition(childNode.X - cumWidth / 2f, childNode.Y);
            }

            return rect;
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return ((IEnumerable<Node>)_nodes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_nodes).GetEnumerator();
        }

        public struct Rect
        {
            public float x, y, width, height;
            public Rect(float x, float y, float width, float height)
            {
                this.x = x; this.y = y; this.width = width; this.height = height;
            }

            public Rect Encapsulate (Rect other)
            {
                float x = Mathf.Min(this.x, other.x);
                float y = Mathf.Min(this.y, other.y);
                float width = Mathf.Max(this.width, other.width);
                float height = Mathf.Max(this.height, other.height);
                return new Rect(x, y, width, height);
            }
        }

        public class Node {

            public float X { get; private set; }
            public float Y { get; private set; }

            public float Width { get; private set; }
            public float Height { get; private set; }

            public Node () { }

            public Node(float x, float y) => SetPosition(x, y);

            public Node(float x, float y, float width, float height)
            {
                SetPosition(x, y);
                SetSize(x, y);
            }

            public void SetPosition (float x, float y) {
                X = x;
                Y = y;
            }

            public void SetSize (float width, float height)
            {
                Width = width;
                Height = height;
            }

            public void ConnectTo (Node other, Edge edge, bool incoming) {
                if (incoming) {
                    edge.Connect(other, this);
                    _incomingEdges.Add(edge);
                }else{
                    edge.Connect(this, other);
                    _outgoingEdges.Add(edge);
                }
            }

            private List<Edge> _outgoingEdges = new List<Edge>();
            private List<Edge> _incomingEdges = new List<Edge>();

            public IEnumerable<Edge> OutgoingEdges => _outgoingEdges;
            public IEnumerable<Edge> IncomingEdges => _incomingEdges;
        }

        public class Edge {

            public void Connect (Node @out, Node @in) {
                OutNode = @out;
                InNode = @in;
            }

            public Node OutNode { get; private set; }
            public Node InNode { get; private set; }
        }
    }
}
