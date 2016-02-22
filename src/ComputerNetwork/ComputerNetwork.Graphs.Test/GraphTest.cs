using FluentAssertions;
using NUnit.Framework;

namespace ComputerNetwork.Graphs.Test
{
    public abstract class GraphTest
    {
        [TestFixture]
        public class AdjacencyTable
        {
            [Test]
            public void UndirectedGraphHasSimetricalEdges()
            {
                var graph = 
                    new GraphBuilder(numberOfVertices: 3,isDirected: false)
                    .AddEdge(0, 2, 4)
                    .ToGraph();

                double.IsPositiveInfinity(graph[0, 0]).Should().BeTrue();
                double.IsPositiveInfinity(graph[0, 1]).Should().BeTrue();
                double.IsPositiveInfinity(graph[0, 2]).Should().BeFalse();
                double.IsPositiveInfinity(graph[2, 0]).Should().BeFalse();
            }

            [Test]
            public void FirectedGraphDoesntHaveSimetricalEdges()
            {
                var graph =
                    new GraphBuilder(numberOfVertices: 3, isDirected: true)
                    .AddEdge(0, 2, 4)
                    .ToGraph();

                double.IsPositiveInfinity(graph[0, 0]).Should().BeTrue();
                double.IsPositiveInfinity(graph[0, 1]).Should().BeTrue();
                double.IsPositiveInfinity(graph[0, 2]).Should().BeFalse();
                double.IsPositiveInfinity(graph[2, 0]).Should().BeTrue();
            }
        }
    }
}