using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.BFA2.Utilities;

namespace Lomztein.BFA2.Tests
{
    [ExecuteAlways]
    public class LayeredGraphDrawingTester : MonoBehaviour
    {
        public bool DoGenerate;
        private LayeredGraphOrganizer _graph;

        // Update is called once per frame
        void Update()
        {
            if (DoGenerate)
            {
                Generate();
                DoGenerate = false;
            }
        }

        void Generate()
        {
            _graph = new LayeredGraphOrganizer();
            var root = _graph.AddNode("Root");
            var root2 = _graph.AddNode("Root2");
            var r = _graph.AddNode("R");
            var r2 = _graph.AddNode("R2");
            var l2 = _graph.AddNode("L2");
            var l = _graph.AddNode("L");
            var rr = _graph.AddNode("RR");
            var ll = _graph.AddNode("LL");
            var c = _graph.AddNode("C");
            var cr = _graph.AddNode("CR");
            var rrr = _graph.AddNode("RRR");
            var rrl = _graph.AddNode("RRL");
            var rrll = _graph.AddNode("RRLL");

            _graph.AddEdge(root, r);
            _graph.AddEdge(root, l);
            _graph.AddEdge(root, r2);
            _graph.AddEdge(root, l2);
            _graph.AddEdge(root2, r2);
            _graph.AddEdge(r, rr);
            _graph.AddEdge(l, ll);
            _graph.AddEdge(l2, rrr);
            _graph.AddEdge(l2, rr);
            _graph.AddEdge(rr, c);
            _graph.AddEdge(ll, c);
            _graph.AddEdge(c, cr);
            _graph.AddEdge(rr, rrr);
            _graph.AddEdge(rrr, cr);
            _graph.AddEdge(rr, rrl);
            _graph.AddEdge(rrl, rrll);
            _graph.AddEdge(cr, rrll);

            _graph.Organize();
        }

        void OnDrawGizmos ()
        {
            if (_graph != null)
            {
                foreach (var pair in _graph)
                {
                    var node = pair.Value;
                    Gizmos.DrawCube(new Vector3(node.X, node.Layer) * 1.5f, Vector3.one);
                    foreach (var child in node.ChildEdges)
                    {
                        Gizmos.DrawLine(new Vector3(node.X, node.Layer) * 1.5f, new Vector3(child.ChildNode.X, child.ChildNode.Layer) * 1.5f);
                    }
                }
            }
        }
    }
}
