namespace LocalNetwork
{
    /// <summary>
    /// class for concrete computer
    /// </summary>
    public class ConcreteComputer
    {
        private AbstractOS operatingSystem;

        public ConcreteComputer(AbstractOS concreteOperatingSystem, bool isInfected)
        {
            operatingSystem = concreteOperatingSystem;
            IsInfected = isInfected;
        }

        /// <summary>
        /// Is computer infected?
        /// </summary>
        public bool IsInfected { get; set; }

        /// <summary>
        /// return infection chance of conrete computer
        /// </summary>
        public int GetInfectionChance()
        {
            return operatingSystem.InfectionChance;
        }

        /// <summary>
        /// return operating system name of concrete computer
        /// </summary>
        public string GetOSName()
        {
            return operatingSystem.OSName;
        }
    }
}