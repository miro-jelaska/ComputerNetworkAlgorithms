using System.Collections.Generic;
using System.Linq;

namespace ComputerNetwork.Graphs
{
    public class GraphWithTracking
    {
        public GraphWithTracking(Graph graph)
        {
            _graph = graph;

            IsDirected         = graph.IsDirected;
            NumberOfVertices   = graph.NumberOfVertices;
            NumberOfEdges      = graph.NumberOfEdges;
            IsVisited          = Enumerable.Range(1, graph.NumberOfVertices).Select(_ => false).ToList();
            CalculatedDistance = Enumerable.Range(1, graph.NumberOfVertices).Select(_ => double.PositiveInfinity).ToList();
        }
        private Graph _graph { get; }

        public bool IsDirected { get; }
        public int NumberOfVertices { get; }
        public int NumberOfEdges { get; }
        public List<bool> IsVisited { get; }
        public List<double> CalculatedDistance { get; }

        public List<int> Verteces => Enumerable.Range(0, NumberOfVertices).ToList();

        public double this[int from, int to] => _graph[from, to];

        public IEnumerable<Distance> AsEnumerable() => _graph.AsEnumerable();
    }
}
