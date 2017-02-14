namespace LocalNetwork
{
    /// <summary>
    /// Linux OS class
    /// </summary>
    public class Linux : AbstractOS
    {
        private const int infectionChance = 15;

        public Linux()
        {
            InfectionChance = infectionChance;
            OSName = "Linux";
        }
    }

}
