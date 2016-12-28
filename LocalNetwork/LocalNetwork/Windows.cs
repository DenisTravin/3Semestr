namespace LocalNetwork
{
    /// <summary>
    /// Windows OS class
    /// </summary>
    public class Windows : AbstractOS
    {
        private const int infectionChance = 70;

        public Windows()
        {
            InfectionChance = infectionChance;
            OSName = "Windows";
        }
    }
}