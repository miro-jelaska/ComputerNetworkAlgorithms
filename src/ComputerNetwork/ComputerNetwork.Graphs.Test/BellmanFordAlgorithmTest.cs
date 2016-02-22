using FluentAssertions;
using NUnit.Framework;

namespace ComputerNetwork.Graphs.Test
{
    public abstract class BellmanFordAlgorithmTest
    {
        [TestFixture]
        public class ShortestPaths
        {
            [Test]
            public void Test01()
            {
                var numberOfVerteces = 5;
                var graph =
                    new GraphBuilder(numberOfVertices: numberOfVerteces, isDirected: true)
                    .AddEdge(3, 0, 7)
                    .AddEdge(3, 4, 6)
                    .AddEdge(2, 3, 2)
                    .AddEdge(0, 2, 9)
                    .AddEdge(0, 1, -3)
                    .AddEdge(4, 0, 8)
                    .AddEdge(2, 1, 7)
                    .AddEdge(1, 4, -2)
                    .AddEdge(4, 2, -4)
                    .ToGraph();
                var startingVertex = 3;

                var result = BellmanFordAlgorithm.ShortestPaths(new GraphWithTracking(graph), startingVertex);
                result.Count.Should().Be(numberOfVerteces);
                result.ShouldBeEquivalentTo(new[] { 7, 4, -2, 0, 2 });
            }

            [Test]
            public void Test02()
            {
                const int numberOfVerteces = 5;
                var graph =
                    new GraphBuilder(numberOfVertices: numberOfVerteces, isDirected: false)
                    .AddEdge(0, 1, 5)
                    .AddEdge(0, 3, 3)
                    .AddEdge(1, 2, 4)
                    .AddEdge(1, 4, 4)
                    .AddEdge(2, 3, 5)
                    .AddEdge(3, 4, 2)
                    .ToGraph();
                const int startingVertex = 0;

                var result = BellmanFordAlgorithm.ShortestPaths(new GraphWithTracking(graph), startingVertex);
                result.Count.Should().Be(numberOfVerteces);
                result.ShouldBeEquivalentTo(new[] { 0, 5, 8, 3, 5 });
            }

            [Test]
            public void Test03()
            {
                const int numberOfVerteces = 4;
                var graph =
                    new GraphBuilder(numberOfVertices: numberOfVerteces, isDirected: false)
                    .AddEdge(0, 1, 1)
                    .AddEdge(0, 2, 2)
                    .AddEdge(0, 3, 17)
                    .AddEdge(1, 2, 5)
                    .AddEdge(2, 3, 4)
                    .ToGraph();
                const int startingVertex = 2;

                var result = BellmanFordAlgorithm.ShortestPaths(new GraphWithTracking(graph), startingVertex);
                result.Count.Should().Be(numberOfVerteces);
                result.ShouldBeEquivalentTo(new[] { 2, 3, 0, 4 });
            }
        }
    }
}