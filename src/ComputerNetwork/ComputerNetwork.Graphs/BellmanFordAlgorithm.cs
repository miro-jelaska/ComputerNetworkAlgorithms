using System.Collections.Generic;
using System.Linq;

namespace ComputerNetwork.Graphs
{
    public static class BellmanFordAlgorithm
    {
        public static IReadOnlyList<double> ShortestPaths(GraphWithTracking graph, int startingVertex)
        {
            graph.CalculatedDistance[startingVertex] = 0;

            var finiteDistances =
                graph.AsEnumerable()
                .Where(_ => !double.IsPositiveInfinity(_.weight))
                .ToList();

            for (var currentVertex = 0; currentVertex < graph.NumberOfVertices; currentVertex++)
                for (var currentEdge = 0; currentEdge < graph.NumberOfEdges; currentEdge++)
                {
                    var newRelaxedDistance = 
                        graph.CalculatedDistance[finiteDistances[currentEdge].fromVertex] + finiteDistances[currentEdge].weight;
                    if (graph.CalculatedDistance[finiteDistances[currentEdge].toVertex] > newRelaxedDistance)
                        graph.CalculatedDistance[finiteDistances[currentEdge].toVertex] = newRelaxedDistance;
                }

            for (var currentEdge = 0; currentEdge < graph.NumberOfEdges; currentEdge++)
            {
                var newRelaxedDistance =
                    graph.CalculatedDistance[finiteDistances[currentEdge].fromVertex] + finiteDistances[currentEdge].weight;
                if (graph.CalculatedDistance[finiteDistances[currentEdge].toVertex] > newRelaxedDistance)
                    graph.CalculatedDistance[finiteDistances[currentEdge].toVertex] = double.NegativeInfinity;
            }

            return graph.CalculatedDistance.ToList();
        }
    }
}
