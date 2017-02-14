namespace GraphiEditor
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class Line
    {
        public Point Start { get; set; }

        public Point End { get; set; }

        public Color LineColor { get; set; }

        /// <summary>
        /// Constructs a line by 2 given points
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Line(Point start, Point end, Color color)
        {
            Start = start;
            End = end;
            LineColor = color;
        }

        /// <summary>
        /// Draw line on picture box
        /// </summary>
        /// <param name="e"></param>
        public void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(LineColor, 2), Start, End);
        }

        /// <summary>
        /// get distatnce between two coordinates
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public double Distance(Point first, Point second)
        {
            return Math.Sqrt((first.X - second.X)* (first.X - second.X) + (first.Y - second.Y)*(first.Y - second.Y));
        }
    }
}
