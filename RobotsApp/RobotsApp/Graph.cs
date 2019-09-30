namespace RopotsApp
{
    /// <summary>
    /// graph structure class
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// number of vertex in graph
        /// </summary>
        private int numberOfVertex;

        /// <summary>
        /// graph array
        /// </summary>
        private bool[,] graph;

        /// <summary>
        /// graph constructor class
        /// </summary>
        /// <param name="matrix">
        /// adjecency matrix
        /// </param>
        /// <param name="numberOfVertex">
        /// number of vertex in graph
        /// </param>
        public Graph(bool[,] matrix, int numberOfVertex)
        {
            graph = matrix;
            this.numberOfVertex = numberOfVertex;
        }

        /// <summary>
        /// get vertex value
        /// </summary>
        /// <returns>does robot stay in current vertex</returns>
        public bool GetVertexValue(int i, int j)
        {
            return graph[i, j];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfVertex()
        {
            return numberOfVertex;
        }
    }
}