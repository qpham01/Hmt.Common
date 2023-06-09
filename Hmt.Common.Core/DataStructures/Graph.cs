﻿using Hmt.Common.Core.Interfaces;
using Hmt.Common.Core.Things;

namespace Hmt.Common.Core.DataStructures;

public class Edge
{
    public IGraphNode From { get; }
    public IGraphNode To { get; }
    public double Weight { get; }

    public Edge(IGraphNode from, IGraphNode to, double weight)
    {
        From = from;
        To = to;
        Weight = weight;
    }
}

public class Node : Thing, IGraphNode
{
    public virtual bool Blocked { get; set; } = false;

    public List<Edge> Edges { get; set; } = new();

    public virtual void AddEdge(IGraphNode to, double distance, bool bidirectional)
    {
        Edges.Add(new Edge(this, to, distance));
        if (bidirectional)
            to.AddEdge(this, distance, false);
    }
}

public class Path
{
    public IGraphNode StartNode { get; }
    public IGraphNode EndNode { get; }
    public List<Edge> Edges { get; }

    public Path(IGraphNode startNode, IGraphNode endNode, List<Edge> edges)
    {
        StartNode = startNode;
        EndNode = endNode;
        Edges = edges;
    }
}

public class Graph<N> where N : class, IGraphNode
{
    public List<N> Nodes { get; set; }

    public Graph()
    {
        Nodes = new List<N>();
    }

    public N? GetNodeByName(string name)
    {
        return Nodes.FirstOrDefault(n => n.Name == name);
    }

    public List<Path> FindAllPaths(N start, N end)
    {
        List<Path> result = new List<Path>();
        FindPathsDFS(start, end, new List<Edge>(), new HashSet<N>() { start }, result);
        return result;
    }

    private void FindPathsDFS(N current, N end, List<Edge> currentPath, HashSet<N> searched, List<Path> result)
    {
        // Explore all possible paths from current node
        var searchEdges = current.Edges.Where(e => !e.To.Blocked);
        foreach (var edge in searchEdges)
        {
            var to = edge.To as N;
            if (to == null)
                continue;
            if (searched.Contains(to))
                continue;
            if (to.Blocked)
                continue;

            // If current node is the destination node, we found a path
            currentPath.Add(edge);
            if (to == end)
            {
                result.Add(new Path(currentPath.First().From, to, new List<Edge>(currentPath)));
                currentPath.Remove(edge);
                continue;
            }
            searched.Add(to);
            FindPathsDFS(to, end, currentPath, searched, result);
            currentPath.Remove(edge); // Remove edge from current path for backtracking
            searched.Remove(to);
        }
    }
}
