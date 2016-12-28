namespace LocalNetwork
{
    /// <summary>
	/// MacOS class
	/// </summary>
	public class MacOS : AbstractOS
    {
        private const int infectionChance = 30;

        public MacOS(int probability = 30)
        {
            InfectionChance = infectionChance;
            OSName = "MacOS";
        }
    }
}
