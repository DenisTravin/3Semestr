namespace GraphicsEditorNamespace
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// handler class for undo and redo operations
    /// </summary>
    public class UndoRedoHandler
    {
        private Stack<List<Line>> states;

        /// <summary>
        /// class constructor
        /// </summary>
        public UndoRedoHandler()
        {
            states = new Stack<List<Line>>();
        }

        /// <summary>
        /// add states state 
        /// </summary>
        /// <param name="list">given state</param>
        public void AddState(List<Line> list) => states.Push(list);

        /// <summary>
        /// get states state 
        /// </summary>
        /// <param name="list">got state</param>
        public List<Line> GetState() => states.Pop();

        /// <summary>
        /// is the states stack empty
        /// </summary>
        /// <returns>true if states is empty, else - false</returns>
        public bool IsEmpty() => states.Count() == 0;
    }
}
