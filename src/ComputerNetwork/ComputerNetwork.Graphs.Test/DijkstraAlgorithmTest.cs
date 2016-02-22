using FluentAssertions;
using NUnit.Framework;

namespace ComputerNetwork.Graphs.Test
{
    public abstract class DijkstraAlgorithmTest
    {
        [TestFixture]
        public class ShortestPaths
        {
            [Test]
            public void Test01()
            {
                const int numberOfVerteces = 9;
                var graph =
                    new GraphBuilder(numberOfVertices: numberOfVerteces, isDirected: false)
                    .AddEdge(0, 1, 10)
                    .AddEdge(0, 3, 4)
                    .AddEdge(0, 4, 17)
                    .AddEdge(1, 2, 3)
                    .AddEdge(1, 4, 7)
                    .AddEdge(3, 4, 15)
                    .AddEdge(3, 6, 2)
                    .AddEdge(4, 7, 3)
                    .AddEdge(6, 7, 3)
                    .AddEdge(2, 4, 5)
                    .AddEdge(2, 5, 6)
                    .AddEdge(5, 7, 1)
                    .AddEdge(2, 8, 7)
                    .AddEdge(7, 8, 19)
                    .AddEdge(5, 8, 27)
                    .ToGraph();
                const int startingVertex = 0;

                var result = DijkstraAlgorithm.ShortestPaths(new GraphWithTracking(graph), startingVertex);
                result.Count.Should().Be(numberOfVerteces);
                result.ShouldBeEquivalentTo(new[] { 0, 10, 13, 4, 12, 10, 6, 9, 20 });
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

                var result = DijkstraAlgorithm.ShortestPaths(new GraphWithTracking(graph), startingVertex);
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

                var result = DijkstraAlgorithm.ShortestPaths(new GraphWithTracking(graph), startingVertex);
                result.Count.Should().Be(numberOfVerteces);
                result.ShouldBeEquivalentTo(new[] { 2, 3, 0, 4 });
            }
        }
    }
}