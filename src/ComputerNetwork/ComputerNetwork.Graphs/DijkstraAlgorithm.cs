using System.Collections.Generic;
using System.Linq;

namespace ComputerNetwork.Graphs
{
    public static class DijkstraAlgorithm
    {
        public static IReadOnlyList<double> ShortestPaths(GraphWithTracking graph, int startingVertex)
        {
            graph.CalculatedDistance[startingVertex] = 0;
            
            for (var i = 0; i < graph.NumberOfVertices - 1; i++)
            {
                var unvisitedVertexWithShortestDistance =
                    graph.Verteces
                        .Where(_ => !graph.IsVisited[_])
                        .Aggregate(
                            (aggregateVertex, currentVertex) =>
                                graph.CalculatedDistance[aggregateVertex] < graph.CalculatedDistance[currentVertex]
                                    ? aggregateVertex
                                    : currentVertex);

                graph.IsVisited[unvisitedVertexWithShortestDistance] = true;
                var distance = graph.CalculatedDistance[unvisitedVertexWithShortestDistance];
                for (var j = 0; j < graph.NumberOfVertices; j++)
                {
                    var newRelaxedDistance = distance + graph[unvisitedVertexWithShortestDistance, j];
                    if (graph.CalculatedDistance[j] > newRelaxedDistance)
                        graph.CalculatedDistance[j] = newRelaxedDistance;
                }
            }

            return graph.CalculatedDistance.ToList();
        }
    }
}
