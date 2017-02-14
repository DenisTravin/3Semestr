namespace GraphiEditor
{
    using System.Collections.Generic;

    /// <summary>
    /// intreface for start and cancel command
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// start command
        /// </summary>
        public abstract void Start(List<Line> shapes);

        /// <summary>
        /// cancel command
        /// </summary>
        public abstract void Cancel(List<Line> shapes);
    }
}
