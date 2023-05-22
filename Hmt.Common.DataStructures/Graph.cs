namespace Hmt.Common.DataStructures;

public class Edge
{
    public Node From { get; }
    public Node To { get; }
    public double Weight { get; }

    public Edge(Node from, Node to, double weight)
    {
        From = from;
        To = to;
        Weight = weight;
    }
}

public class Node
{
    public string Name { get; set; }
    public bool Blocked { get; set; }
    public List<Edge> Edges { get; set; } = new List<Edge>();

    public void AddEdge(Node to, double distance, bool bidirectional)
    {
        Edges.Add(new Edge(this, to, distance));
        if (bidirectional)
            to.AddEdge(this, distance, false);
    }

    public Node(string name)
    {
        Name = name;
        Blocked = false;
    }
}

public class Path
{
    public Node StartNode { get; }
    public Node EndNode { get; }
    public List<Edge> Edges { get; }

    public Path(Node startNode, Node endNode, List<Edge> edges)
    {
        StartNode = startNode;
        EndNode = endNode;
        Edges = edges;
    }
}

public class Graph
{
    public List<Node> Nodes { get; set; }

    public Graph()
    {
        Nodes = new List<Node>();
    }

    public List<Path> FindAllPaths(Node start, Node end)
    {
        List<Path> result = new List<Path>();
        FindPathsDFS(start, end, null, new List<Edge>(), new HashSet<Node>() { start }, result);
        return result;
    }

    private void FindPathsDFS(
        Node current,
        Node end,
        Node? previous,
        List<Edge> currentPath,
        HashSet<Node> searched,
        List<Path> result
    )
    {
        // Explore all possible paths from current node
        var searchEdges = current.Edges.Where(e => e.To != previous && !e.To.Blocked);
        foreach (var edge in searchEdges)
        {
            var to = edge.To;
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
            FindPathsDFS(to, end, current, currentPath, searched, result);
            currentPath.Remove(edge); // Remove edge from current path for backtracking
            searched.Remove(to);
        }
    }
}
