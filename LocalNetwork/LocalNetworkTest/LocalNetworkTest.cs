namespace LocalNetwork
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

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
            Assert.IsFalse(localNetwork.IsNetworkInfected());
            for (var i = 0; i < localComputers.Count; i++)
            {
                localComputers[i].IsInfected = true;
            }
            Assert.IsTrue(localNetwork.IsNetworkInfected());
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
            Random randomElement = new Random();
            Func<int> virusFunc = () => randomElement.Next(0, 100);
            localNetwork.Infection(virusFunc);
            Assert.IsTrue(localNetwork.IsNetworkInfected());
        }

        [TestMethod]
        public void ImpossibleVirusSimulationTest()
        {
            bool[,] adjMatrix = new bool[3, 3] { { true, false, false }, { false, true, true }, { false, true, true } };
            var localComputers = new List<ConcreteComputer>
            {
                new ConcreteComputer(new Linux(), true),
                new ConcreteComputer(new MacOS(), false),
                new ConcreteComputer(new Windows(), false)
            };
            var localNetwork = new LocalNetwork(localComputers, adjMatrix);
            Func<int> virusFunc = () => 0;
            localNetwork.InfectionStep(virusFunc);
            Assert.IsFalse(localNetwork.IsNetworkInfected());
        }

        [TestMethod]
        public void FirstVirusSimulationWithotRandomTest()
        {
            bool[,] adjMatrix = new bool[3, 3] { { true, true, false }, { true, true, true }, { false, true, true } };
            var localComputers = new List<ConcreteComputer>
            {
                new ConcreteComputer(new Linux(), true),
                new ConcreteComputer(new MacOS(), false),
                new ConcreteComputer(new Windows(), false)
            };
            var localNetwork = new LocalNetwork(localComputers, adjMatrix);
            Func<int> virusFunc = () => 0;
            localNetwork.InfectionStep(virusFunc);
            Assert.IsTrue(localComputers[1].IsInfected);
            Assert.IsFalse(localNetwork.IsNetworkInfected());
            localNetwork.InfectionStep(virusFunc);
            Assert.IsTrue(localNetwork.IsNetworkInfected());
        }

        [TestMethod]
        public void SecondVirusSimulationWithotRandomTest()
        {
            bool[,] adjMatrix = new bool[5, 5] { { true, true, false, false, false }, { true, true, true, false, true }, { false, true, true, true, false }, { false, false, true, true, false}, { false, true, false, false, true} };
            var localComputers = new List<ConcreteComputer>
            {
                new ConcreteComputer(new Linux(), true),
                new ConcreteComputer(new MacOS(), false),
                new ConcreteComputer(new Windows(), false),
                new ConcreteComputer(new MacOS(), false),
                new ConcreteComputer(new Windows(), false)
            };
            var localNetwork = new LocalNetwork(localComputers, adjMatrix);
            Func<int> virusFunc = () => 0;
            localNetwork.InfectionStep(virusFunc);
            Assert.IsTrue(localComputers[1].IsInfected);
            Assert.IsFalse(localComputers[2].IsInfected);
            Assert.IsFalse(localComputers[3].IsInfected);
            Assert.IsFalse(localComputers[4].IsInfected);
            localNetwork.InfectionStep(virusFunc);
            Assert.IsTrue(localComputers[2].IsInfected);
            Assert.IsFalse(localComputers[3].IsInfected);
            Assert.IsTrue(localComputers[4].IsInfected);
            localNetwork.InfectionStep(virusFunc);
            Assert.IsTrue(localNetwork.IsNetworkInfected());
        }

    }
}
