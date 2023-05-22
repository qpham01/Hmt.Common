using Hmt.Common.Core.DataStructures;

namespace Hmt.Common.UnitTests.Core.DataStructures;

public class GraphTests
{
    class TestNode : Node
    {
        public TestNode(string name)
        {
            Name = name;
        }
    }

    private TestGraph _graph;
    private TestNode _safeHouse;
    private TestNode _pontLeveque;
    private TestNode _pontDuNord;
    private TestNode _poorDistrict;
    private TestNode _blackMarket;
    private TestNode _grocer;
    private TestNode _doctor;

    class TestGraph : Graph<TestNode> { }

    [SetUp]
    public void SetUp()
    {
        // create nodes
        _pontLeveque = new TestNode("Pont Leveque");
        _poorDistrict = new TestNode("Poor District");
        _pontDuNord = new TestNode("Pont Du Nord");
        _blackMarket = new TestNode("Black Market");
        _grocer = new TestNode("Grocer");
        _safeHouse = new TestNode("Safe House");
        _doctor = new TestNode("Doctor");

        // create graph and add nodes
        _graph = new TestGraph();
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
    public void FindAllPaths_Complex_Paths(string startName, string endName, string? blockedName, int pathCount)
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
