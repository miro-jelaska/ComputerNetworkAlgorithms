using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace ComputerNetwork.Graphs
{
    public class Graph
    {
        public Graph(double[,] adjacencyTable, bool isDirected)
        {
            adjacencyTable.GetLength(0).Should().Be(adjacencyTable.GetLength(1));

            _adjacencyTable  = adjacencyTable;
            IsDirected       = isDirected;
            NumberOfVertices = adjacencyTable.GetLength(0);
            NumberOfEdges    = adjacencyTable.Cast<double>().Count(edgeDistance => !double.IsPositiveInfinity(edgeDistance));
        }
        private double[,] _adjacencyTable { get; }

        public double this[int from, int to] => _adjacencyTable[from, to];
        public bool IsDirected { get; }
        public int NumberOfVertices { get; }
        public int NumberOfEdges { get; }

        public bool IsConnected(int fromVertex, int toVertex) => !double.IsPositiveInfinity(_adjacencyTable[fromVertex, toVertex]);


        public IEnumerable<Distance> AsEnumerable()
        {
            for (var from = 0; from < NumberOfVertices; from++)
                for (var to = 0; to < NumberOfVertices; to++)
                    yield return new Distance
                    {
                        fromVertex = from,
                        toVertex = to,
                        weight = _adjacencyTable[from, to]
                    };
        }
    }
}
