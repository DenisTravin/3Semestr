using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalNetwork
{
    [TestClass]
    public class LocalNetworkTests
    {
        [TestMethod]
        public void SystemStatusCheck()
        {
            bool[,] adjMatrix = new bool[3, 3] { { true, true, true }, { true, true, true }, { true, true, true } };
            var localComputers = new List<ConcreteComputer>
            {
                new ConcreteComputer(new Linux(), false),
                new ConcreteComputer(new MacOS(), false),
                new ConcreteComputer(new Windows(), false)
            };
            var localNetwork = new LocalNetwork(localComputers, adjMatrix);
            Assert.IsFalse(localNetwork.SystemStatus());
            for (var i = 0; i < localComputers.Count; i++)
            {
                localComputers[i].IsInfected = true;
            }
            Assert.IsTrue(localNetwork.SystemStatus());
        }

        [TestMethod]
        public void SimulationTest()
        {
            bool[,] adjMatrix = new bool[3, 3] { { true, true, false }, { true, true, true }, { false, true, true } };
            var localComputers = new List<ConcreteComputer>
            {
                new ConcreteComputer(new Linux(), true),
                new ConcreteComputer(new MacOS(), false),
                new ConcreteComputer(new Windows(), false)
            };
            var localNetwork = new LocalNetwork(localComputers, adjMatrix);
            localNetwork.Infection();
            Assert.IsTrue(localNetwork.SystemStatus());
        }

        [TestMethod]
        public void ImpossibleVirusSimulationTest()
        {
            const int steps = 100;
            bool[,] adjMatrix = new bool[3, 3] { { true, false, false }, { false, true, true }, { false, true, true } };
            var localComputers = new List<ConcreteComputer>
            {
                new ConcreteComputer(new Linux(), true),
                new ConcreteComputer(new MacOS(), false),
                new ConcreteComputer(new Windows(), false)
            };
            var localNetwork = new LocalNetwork(localComputers, adjMatrix);
            for (var i = 0; i < steps; i++)
            {
                localNetwork.InfectionStep();
            }
            Assert.IsFalse(localNetwork.SystemStatus());
        }

    }
}
