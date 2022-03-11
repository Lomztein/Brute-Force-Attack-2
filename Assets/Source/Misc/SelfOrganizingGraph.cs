using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class SelfOrganizingGraph
    {
        private List<Node> _nodes;

        public Node AddNode (int initialX, int initialY) {
            Node node = new Node();
            node.SetPosition(initialX, initialY);
            _nodes.Add(node);
            return node;
        }

        public void AddEdge (Node from, Node to) {
            Edge edge = new Edge();
            to.ConnectTo(from, edge, true);
            from.ConnectTo(to, edge, false);
        }

        public IEnumerable<Node> GetRootNodes () => _nodes.Where(x => x.IncomingEdges.Count() == 0);

        public void Organize(int maxIters) {
            // Goal #1: No two nodes may share both X and Y values.
            // Goal #2: All children must have a lower Y-value than their parents.
            // Goal #3: Min and max length edges between a parent and their children should be as close as possible.

            // Sort all according to heritage by traversing from root nodes and downwards.
            bool anyChanged = true;
            while (anyChanged) {

            }

        }

        public class Node {

            public int X { get; private set; }
            public int Y { get; private set; }

            public void SetPosition (int x, int y) {
                X = x;
                Y = y;
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

            private List<Edge> _outgoingEdges;
            private List<Edge> _incomingEdges;

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
