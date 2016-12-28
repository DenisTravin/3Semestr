using System;
using System.Collections.Generic;
using System.Threading;

namespace LocalNetwork
{
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
        /// ramdom element for simulation
        /// </summary>
        private Random randomElement = new Random();

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
        public void InfectionStep()
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
                            if (randomElement.Next(0, 100) < allComputers[j].GetInfectionChance())
                            {
                                allComputers[j].IsInfected = true;
                                infectedComputers.Add(allComputers[j]);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// check local network status   
        /// </summary>
        /// <returns>Does all computers infected?</returns>
        public bool SystemStatus()
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
        public void Infection()
        {
            int step = 1;
            while (!SystemStatus())
            {
                InfectionStep();
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