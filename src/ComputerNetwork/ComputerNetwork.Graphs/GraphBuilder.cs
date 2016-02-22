namespace ComputerNetwork.Graphs
{
    public class GraphBuilder
    {
        public GraphBuilder(int numberOfVertices, bool isDirected)
        {
            NumberOfVertices = numberOfVertices;
            IsDirected       = isDirected;

            adjacencyTable = new double[numberOfVertices, numberOfVertices];
            for(var i=0; i < numberOfVertices; i++)
                for (var j = 0; j < numberOfVertices; j++)
                    adjacencyTable[i, j] = double.PositiveInfinity;
        }
        public int NumberOfVertices { get; }
        public bool IsDirected { get; }
        private double[,] adjacencyTable { get; }

        public GraphBuilder AddEdge(int sourceId, int destinationId, double weight)
        {
            adjacencyTable[sourceId, destinationId] = weight;
            if (!IsDirected)
                adjacencyTable[destinationId, sourceId] = weight;

            return this;
        }

        public Graph ToGraph() => new Graph(adjacencyTable, IsDirected);
    }
}
