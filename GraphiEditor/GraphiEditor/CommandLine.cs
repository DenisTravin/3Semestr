namespace GraphiEditor
{
    using System.Collections.Generic;

    /// <summary>
    /// concrete command for line
    /// </summary>
    public class CommandLine : Command
    {
        private string name;
        private Line line;

        public CommandLine(Line line, string commandName)
        {
            this.line = line;
            name = commandName;
        }

        public override void Start(List<Line> lines)
        {
            if (name == "Add")
            {
                lines.Add(line);
            }
            else
            {
                lines.Remove(line);
            }
        }

        public override void Cancel(List<Line> lines)
        {
            if (name != "Add")
            {
                lines.Add(line);
            }
            else
            {
                lines.Remove(line);
            }
        }

    }
}
