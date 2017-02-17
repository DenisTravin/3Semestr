namespace GraphicsEditorNamespace
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Drawing;

    /// <summary>
    /// graphics editor main logic class
    /// </summary>
    public class GraphicsEditorLogic
    {
        private const int pxConst = 5;

        private List<Line> lines;
        

        /// <summary>
        /// class constructor
        /// </summary>
        public GraphicsEditorLogic()
        {
            lines = new List<Line>();
        }

        /// <summary>
        /// get current logic state
        /// </summary>
        /// <returns>got state</returns>
        public List<Line> GetState()
        {
            List<Line> outputList = new List<Line>();
            foreach (var item in lines)
            {
                outputList.Add(item);
            }
            return outputList;
        }

        /// <summary>
        /// set logic state
        /// </summary>
        /// <param name="givenList">given state</param>
        public void SetState(List<Line> givenList)
        {
            lines = givenList;
        }

        /// <summary>
        /// add new line
        /// </summary>
        /// <param name="newLine">added line</param>
        public void AddLine(Line newLine)
        {
            lines.Add(newLine);
        }

        /// <summary>
        /// remove line
        /// </summary>
        /// <param name="line">removed line</param>
        private void RemoveLine(Line line)
        {
            lines.Remove(line);
        }

        /// <summary>
        /// draw all lines
        /// </summary>
        /// <param name="e"></param>
        public void Draw(PaintEventArgs e)
        {
            foreach (var item in lines)
            {
                item.Draw(e);
            }
        }

        /// <summary>
        /// Remove the nearest line to the given point
        /// </summary>
        /// <param name="point"> point </param>
        /// <returns> 1 - if such line exists, 0 - otherwise </returns>
        public bool RemoveLine(PointF point)
        {
            foreach (var item in lines)
            {
                if (item.Distance(point) < pxConst)
                {
                    RemoveLine(item);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// calculatee distance between 2 points
        /// </summary>
        /// <param name="first">first point</param>
        /// <param name="second">second point</param>
        /// <returns>calculated distance</returns>
        private double DistanceBetweenPoints(PointF first, PointF second) => Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));

        /// <summary>
        /// find line that was dragged by user and return its not dragged end
        /// </summary>
        /// <param name="point">dragged point</param>
        /// <returns>not dragged line end</returns>
        public PointF FindNotDraggedLineEnd(PointF point)
        {
            foreach (var item in lines)
            {
                if (DistanceBetweenPoints(item.Start, point) < pxConst)
                {
                    var temp = item.End;
                    RemoveLine(item);
                    return temp;
                }
                else if (DistanceBetweenPoints(item.End, point) < pxConst)
                {
                    var temp = item.Start;
                    RemoveLine(item);
                    return temp;
                }
            }
            return new PointF();
        }
    }
}
