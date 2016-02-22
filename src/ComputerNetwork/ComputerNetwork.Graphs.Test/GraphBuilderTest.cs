using FluentAssertions;
using NUnit.Framework;

namespace ComputerNetwork.Graphs.Test
{
    public abstract class GraphBuilderTest
    {
        [TestFixture]
        public class NumberOfVerteces
        {
            [Test]
            public void NumberOfVertecesAreCorrect()
            {
                var graph = 
                    new GraphBuilder(numberOfVertices: 3,isDirected: false)
                    .AddEdge(0, 2, 4);

                graph.NumberOfVertices.Should().Be(3);
            }
        }
    }
}