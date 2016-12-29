using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RopotsApp
{
    [TestClass]
    public class RopotAppTests
    {
        [TestClass]
        public class MatrixTests
        {
            [TestMethod]
            public void SimpleImposibleSystemTest()
            {
                bool[,] matrix = new bool[2, 2] { { true, true }, { true, true } };
                bool[] robotPosition = new bool[2] { true, true };
                Graph graph = new Graph(matrix, robotPosition.Length);
                RobotsTask robots = new RobotsTask(graph, robotPosition);
                Assert.IsFalse(robots.SystemStatus());
            }

            [TestMethod]
            public void SimplePosibleSystemTest()
            {
                bool[,] matrix = new bool[3, 3] { { true, true, true }, { true, true, true }, { true, true, true } };
                bool[] robotPosition = new bool[3] { true, false, true };
                Graph graph = new Graph(matrix, robotPosition.Length);
                RobotsTask robots = new RobotsTask(graph, robotPosition);
                Assert.IsTrue(robots.SystemStatus());
            }

            [TestMethod]
            public void NormalImposibleSystemTest()
            {
                bool[,] matrix = new bool[4, 4] { { true, false, true, true }, { false, true, true, false }, { true, true, true, false }, { true, false, false, true } };
                bool[] robotPosition = new bool[4] { true, true, true, false };
                Graph graph = new Graph(matrix, robotPosition.Length);
                RobotsTask robots = new RobotsTask(graph, robotPosition);
                Assert.IsFalse(robots.SystemStatus());
            }

            [TestMethod]
            public void NormalPosibleSystemTest()
            {
                bool[,] matrix = new bool[4, 4] { { true, false, true, true }, { false, true, false, true }, { true, false, true, true }, { true, true, true, true } };
                bool[] robotPosition = new bool[4] { true, true, true, false };
                Graph graph = new Graph(matrix, robotPosition.Length);
                RobotsTask robots = new RobotsTask(graph, robotPosition);
                Assert.IsTrue(robots.SystemStatus());
            }
        }
    }
}
