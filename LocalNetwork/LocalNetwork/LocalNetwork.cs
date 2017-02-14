namespace LocalNetwork
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// local network simulating class
    /// </summary>
    public class LocalNetwork
    {
        /// <summary>
        /// all computers
        /// </summary>
        private List<ConcreteComputer> allComputers;

        /// <summary>
        /// infected computers
        /// </summary>
        private List<ConcreteComputer> infectedComputers;

        /// <summary>
        /// linked computers
        /// </summary>
        private bool[,] linkedComputers;

        /// <summary>
        /// LocalNetwork constructor class
        /// </summary>
        /// <param name="localNetwork">list of concrete computers</param>
        /// <param name="adjMatrix">Adjacency matrix</param>
        public LocalNetwork(List<ConcreteComputer> localNetwork, bool[,] adjMatrix)
        {
            allComputers = localNetwork;
            linkedComputers = adjMatrix;
        }

        /// <summary>
        /// make infection step
        /// </summary>
        public void InfectionStep(Lambda randomElement)
        {
            infectedComputers = new List<ConcreteComputer>();
            for (var i = 0; i < allComputers.Count; i++)
            {
                if (allComputers[i].IsInfected)
                {
                    for (var j = 0; j < allComputers.Count; j++)
                    {
                        if (i != j && linkedComputers[i, j] && !allComputers[j].IsInfected && !infectedComputers.Contains(allComputers[j]))
                        {
                            if (randomElement.ReturnValue < allComputers[j].GetInfectionChance())
                            {
                                infectedComputers.Add(allComputers[j]);
                            }
                        }
                    }
                }
            }
            foreach(var computer in infectedComputers)
            {
                computer.IsInfected = true;
            }
        }

        /// <summary>
        /// check local network status   
        /// </summary>
        /// <returns>Does all computers infected?</returns>
        public bool IsNetworkInfected()
        {
            int numberOfInfectedComputers = 0;
            foreach (var localComputer in allComputers)
            {
                if (localComputer.IsInfected)
                {
                    numberOfInfectedComputers++;
                }
            }
            return numberOfInfectedComputers == allComputers.Count;
        }

        /// <summary>
        /// Simulates infection of computers in local network.
        /// </summary>
        public void Infection(bool isRandom)
        {
            int step = 1;
            while (!IsNetworkInfected())
            {
                Lambda randomElement = new Lambda(isRandom);
                InfectionStep(randomElement);
                Console.WriteLine("{0} step:", step);
                for (var i = 0; i < allComputers.Count; i++)
                {
                    Console.WriteLine("{0} computer: ", i, "OS name: " + allComputers[i].GetOSName() + "; " + "Infected: " + allComputers[i].IsInfected);
                }
                Console.WriteLine();
                step++;
            }
            Console.WriteLine("All local network are infected/");
        }
    }
}