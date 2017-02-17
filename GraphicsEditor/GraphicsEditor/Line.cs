namespace GraphicsEditorNamespace
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// line class
    /// </summary>
    public class Line
    {
        /// <summary>
        /// line start point
        /// </summary>
        public PointF Start { get; set; }

        /// <summary>
        /// line end point
        /// </summary>
        public PointF End { get; set; }

        /// <summary>
        /// line constructor
        /// </summary>
        /// <param name="start">line start</param>
        /// <param name="end">line end</param>
        public Line(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// draw line in picture box
        /// </summary>
        /// <param name="e"></param>
        public void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 3), Start, End);
        }

        private double DistanceBetweenPoints(PointF first, PointF second) => Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));

        /// <summary>
        /// distance between line and the given point
        /// </summary>
        /// <param name="point">given point</param>
        /// <returns>counted distance</returns>
        public double Distance(PointF point)
        {
            var maxX = Math.Max(Start.X, End.X);
            var maxY = Math.Max(Start.Y, End.Y);
            var minX = Math.Min(Start.X, End.X);
            var minY = Math.Min(Start.Y, End.Y);

            if (maxX < point.X && maxY < point.Y)
            {
                return Math.Min(DistanceBetweenPoints(Start, point), DistanceBetweenPoints(End, point));
            }
            else if (maxX > point.X && minY > point.Y)
            {
                return Math.Min(DistanceBetweenPoints(Start, point), DistanceBetweenPoints(End, point));
            }
            else if (minX > point.X && maxY < point.Y)
            {
                return Math.Min(DistanceBetweenPoints(Start, point), DistanceBetweenPoints(End, point));
            }
            else if (minX > point.X && minY > point.Y)
            {
                return Math.Min(DistanceBetweenPoints(Start, point), DistanceBetweenPoints(End, point));
            }

            double A = (Start.Y - End.Y);
            double B = (End.X - Start.X);
            double C = (End.Y - Start.Y) * Start.X + (Start.X - End.X) * Start.Y;
            return Math.Abs(A * point.X + B * point.Y + C) / (Math.Sqrt(A * A + B * B));
        }
    }
}
