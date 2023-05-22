namespace Hmt.Common.DataStructures.UnitTests;

public class GraphTests
{
    private Graph _graph;
    private Node _safeHouse;
    private Node _pontLeveque;
    private Node _pontDuNord;
    private Node _poorDistrict;
    private Node _blackMarket;
    private Node _grocer;
    private Node _doctor;

    [SetUp]
    public void SetUp()
    {
        // create nodes
        _pontLeveque = new Node("Pont Leveque");
        _poorDistrict = new Node("Poor District");
        _pontDuNord = new Node("Pont Du Nord");
        _blackMarket = new Node("Black Market");
        _grocer = new Node("Grocer");
        _safeHouse = new Node("Safe House");
        _doctor = new Node("Doctor");

        // create graph and add nodes
        _graph = new Graph();
        _graph.Nodes.AddRange(new[] { _safeHouse, _pontLeveque, _poorDistrict, _pontDuNord, _blackMarket, _grocer });
        AddEdgesInALine();
    }

    [Test]
    public void FindAllPaths_Returns_All_Paths()
    {
        var paths = _graph.FindAllPaths(_safeHouse, _grocer);
        Assert.IsTrue(paths.Any());
        foreach (var path in paths)
        {
            Assert.That(path.StartNode, Is.EqualTo(_safeHouse));
            Assert.That(path.EndNode, Is.EqualTo(_grocer));
            Assert.That(path.Edges.Count, Is.EqualTo(_graph.Nodes.Count - 1));
        }
    }

    [Test]
    public void FindAllPaths_When_Node_Is_Blocked_Returns_No_Path()
    {
        _pontDuNord.Blocked = true;
        var paths = _graph.FindAllPaths(_safeHouse, _grocer);
        Assert.IsFalse(paths.Any());
    }

    [TestCase("Doctor", "Safe House", null, 3)]
    [TestCase("Doctor", "Safe House", "Poor District", 1)]
    [TestCase("Doctor", "Safe House", "Pont Leveque", 0)]
    [TestCase("Black Market", "Safe House", null, 3)]
    [TestCase("Black Market", "Safe House", "Pont Du Nord", 2)]
    [TestCase("Poor District", "Safe House", null, 3)]
    [TestCase("Poor District", "Safe House", "Pont Du Nord", 2)]
    [TestCase("Pont Du Nord", "Safe House", null, 4)]
    [TestCase("Pont Du Nord", "Safe House", "Black Market", 1)]
    [TestCase("Pont Du Nord", "Safe House", "Poor District", 1)]
    [TestCase("Pont Leveque", "Safe House", null, 3)]
    [TestCase("Pont Leveque", "Safe House", "Black Market", 1)]
    [TestCase("Pont Leveque", "Safe House", "Poor District", 1)]
    public void FindAllPaths_Compplex_Paths(string startName, string endName, string? blockedName, int pathCount)
    {
        AddAdditionalEdges();
        var start = _graph.Nodes.FirstOrDefault(n => n.Name == startName);
        var end = _graph.Nodes.FirstOrDefault(n => n.Name == endName);
        if (!string.IsNullOrWhiteSpace(blockedName))
        {
            var blocked = _graph.Nodes.FirstOrDefault(n => n.Name == blockedName);
            if (blocked != null)
                blocked.Blocked = true;
        }
        var paths = _graph.FindAllPaths(start!, end!);
        Assert.That(paths.Count, Is.EqualTo(pathCount));
    }

    private void AddEdgesInALine()
    {
        for (int i = 0; i < _graph.Nodes.Count - 1; i++)
        {
            var node = _graph.Nodes[i];
            node.AddEdge(_graph.Nodes[i + 1], 1.0, true);
        }
    }

    private void AddAdditionalEdges()
    {
        _graph.Nodes.Add(_doctor);
        _poorDistrict.AddEdge(_blackMarket, 1.0, true);
        _grocer.AddEdge(_safeHouse, 1.0, true);
        _pontLeveque.AddEdge(_doctor, 1.0, true);
    }
}
