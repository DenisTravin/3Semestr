namespace RopotsApp
{
    /// <summary>
    /// main logic class
    /// </summary>
    public class RobotsTask
    {
        private Graph graph;
        private int countRobots = 0;
        private int countMarkedRobots = 0;
        private bool[] vertexWithRobot;
        private bool[] markedVerteces;

        /// <summary>
        /// robots task constructor
        /// </summary>
        /// <param name="graph">input graph</param>
        /// <param name="positionAdd">does robot stay on this position</param>
        public RobotsTask(Graph graph, bool[] vertexWithRobot)
        {
            this.graph = graph;
            this.vertexWithRobot = vertexWithRobot;
        }

        /// <summary>
        /// initial method
        /// </summary>
        private void InitialGraph()
        {
            for (var i = 0; i < graph.GetNumberOfVertex(); i++)
            {
                if (vertexWithRobot[i])
                {
                    countRobots++;
                }
            }
            markedVerteces = new bool[graph.GetNumberOfVertex()];
            markedVerteces[0] = true;
            for (var i = 1; i < graph.GetNumberOfVertex(); i++)
            {
                markedVerteces[i] = false;
            }
            countMarkedRobots++;
            MarkVertex(0);
        }

        /// <summary>
        /// recursive mar vertex method
        /// </summary>
        /// <param name="currentVertex">vertex for consider</param>
        private void MarkVertex(int currentVertex)
        {
            for (var i = 0; i < graph.GetNumberOfVertex(); i++)
            {
                if (!markedVerteces[i] && IsWayExist(currentVertex, i))
                {
                    if (vertexWithRobot[i])
                    {
                        countMarkedRobots++;
                    }
                    markedVerteces[i] = true;
                    MarkVertex(i);
                }
            }
        }

        /// <summary>
        /// check availability of way between input vertexes
        /// </summary>
        private bool IsWayExist(int first, int second)
        {
            for (var i = 0; i < graph.GetNumberOfVertex(); i++)
            {
                if ((i != first) && (i != second) && (graph.GetVertexValue(first, i)) && (graph.GetVertexValue(second, i)))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// check does robots can destroy each other
        /// </summary>
        public bool SystemStatus()
        {
            InitialGraph();
            return ((countMarkedRobots != 1) && (countRobots - countMarkedRobots != 1));
        }
    }
}