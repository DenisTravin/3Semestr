namespace LocalNetwork
{
    /// <summary>
    /// abstract operation system class
    /// </summary>
    public abstract class AbstractOS
    {
        /// <summary>
        /// name of operation system
        /// </summary>
        public string OSName { get; set; }

        /// <summary>
        /// System infection chance
        /// </summary>
        public int InfectionChance { get; set; }
    }
}