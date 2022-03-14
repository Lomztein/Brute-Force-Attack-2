using Lomztein.BFA2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SelfOrganizingGraphTester : MonoBehaviour
{
    private SelfOrganizingGraph _graph;

    public bool Regenerate;

    private void Update()
    {
        if (Regenerate)
        {
            GenerateGraph();
        }
    }

    void GenerateGraph()
    {
        // Generate test graph;
        _graph = new SelfOrganizingGraph();
        var root = _graph.AddNode();
        var r = _graph.AddNode();
        _graph.AddEdge(root, r);
        var l = _graph.AddNode();
        _graph.AddEdge(root, l);
        var rl = _graph.AddNode();
        _graph.AddEdge(r, rl);
        _graph.AddEdge(l, rl);
        var rr = _graph.AddNode();
        var rrr = _graph.AddNode();
        var rrl = _graph.AddNode();
        _graph.AddEdge(r, rr);
        _graph.AddEdge(rr, rrr);
        _graph.AddEdge(rr, rrl);

        _graph.Organize();
    }

    private void OnDrawGizmos()
    {
        foreach (var node in _graph)
        {
            Gizmos.DrawCube(new Vector3(node.X, node.Y), Vector3.one);
            foreach (var outgoing in node.OutgoingEdges)
            {
                Gizmos.DrawLine(new Vector3(outgoing.InNode.X, outgoing.InNode.Y), new Vector3(outgoing.OutNode.X, outgoing.OutNode.Y));
            }
        }
    }
}
