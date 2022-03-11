using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2
{
    [ExecuteAlways]
    public class LayeredGraphDrawingTester : MonoBehaviour
    {
        public bool DoGenerate;
        private SugiyamaGraphOrganizer _graph;

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
            _graph = new SugiyamaGraphOrganizer();
            var root = _graph.AddNode();
            var r = _graph.AddNode();
            var l = _graph.AddNode();
            var rr = _graph.AddNode();
            var ll = _graph.AddNode();
            var c = _graph.AddNode();
            var cr = _graph.AddNode();
            var rrr = _graph.AddNode();
            var rrl = _graph.AddNode();
            var rrll = _graph.AddNode();

            _graph.AddEdge(root, r);
            _graph.AddEdge(root, l);
            _graph.AddEdge(r, rr);
            _graph.AddEdge(l, ll);
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
                foreach (var node in _graph)
                {
                    Gizmos.DrawCube(new Vector3(node.X, node.Y), Vector3.one);
                    foreach (var child in node.ChildEdges)
                    {
                        Gizmos.DrawLine(new Vector3(node.X, node.Y), new Vector3(child.ChildNode.X, child.ChildNode.Y));
                    }
                }
            }
        }
    }
}
