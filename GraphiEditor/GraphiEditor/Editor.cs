namespace GraphiEditor
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Editor : Form
    {
        private UndoRedo unre = new UndoRedo();
        private List<Line> lines = new List<Line>();
        private Color lineColor = Color.Black;
        private bool canPaintLine = false;
        private bool isStartCoordinate = false;
        private bool manipulationFlag = false;
        private bool isReDrawLineHasEnd = false;
        private Point redrowingLineEnd;
        private Point lastClickCoordinate;
        private Point currentMousePosition;
        
        public Editor()
        {
            InitializeComponent();
            undoButton.Enabled = false;
            redoButton.Enabled = false;
        }

        private void PictureBoxPaintHandler(object sender, PaintEventArgs e)
        {
            ReDrawAll(e);
            if (canPaintLine && isStartCoordinate)
            {
                e.Graphics.DrawLine(new Pen(lineColor, 2), lastClickCoordinate, currentMousePosition);
            }
            else if (manipulationFlag && isReDrawLineHasEnd)
            {
                e.Graphics.DrawLine(new Pen(lineColor, 2), redrowingLineEnd, currentMousePosition);
            }
        }

        private void PictureBoxMouseMoveHandler(object sender, MouseEventArgs e)
        {
            currentMousePosition.X = e.X;
            currentMousePosition.Y = e.Y;
            if (canPaintLine && isStartCoordinate || (manipulationFlag && isReDrawLineHasEnd))
            {
                editBox.Invalidate();
            }
        }

        private void PictureBoxMouseClickHandler(object sender, MouseEventArgs e)
        {
            var beforeClic = lastClickCoordinate;
            lastClickCoordinate.X = e.X;
            lastClickCoordinate.Y = e.Y;
            if (canPaintLine && isStartCoordinate)
            {
                lines.Add(new Line(beforeClic, lastClickCoordinate, lineColor));
                unre.AddCommand(new CommandLine(lines[lines.Count - 1], "Add"), true);
                CheckStackUnRe();
                canPaintLine = false;
                isStartCoordinate = false;
            }
            else if (!canPaintLine && !isStartCoordinate)
            {
                canPaintLine = true;
                isStartCoordinate = true;
            }
            else if(manipulationFlag && !isReDrawLineHasEnd)
            {
                ActionForLineReDrawing(lastClickCoordinate);
            }
            else if (manipulationFlag && isReDrawLineHasEnd)
            {
                manipulationFlag = false;
                isReDrawLineHasEnd = false;
                isStartCoordinate = false;
                lines.Add(new Line(redrowingLineEnd, lastClickCoordinate, lineColor));
                unre.AddCommand(new CommandLine(lines[lines.Count - 1], "Add"), true);
                CheckStackUnRe();
            }
        }

        /// <summary>
        /// funcion for find near line with last click coordinate and delete this line 
        /// </summary>
        /// <param name="lastClick"></param>
        private void ActionForLineReDrawing(Point lastClick)
        {
            var temp = new Line(lastClick, lastClick, lineColor);
            foreach (var line in lines)
            {
                if (line.Distance(line.Start, lastClick) < 8)
                {
                    redrowingLineEnd = line.End;
                    temp = line;
                }
                else if (line.Distance(line.End, lastClick) < 8)
                {
                    redrowingLineEnd = line.Start;
                    temp = line;
                }
            }
            
            if (!redrowingLineEnd.IsEmpty)
            {               
                unre.AddCommand(new CommandLine(temp, "Remove"), true);
                lines.Remove(temp);
                isReDrawLineHasEnd = true;
            }
        }

        /// <summary>
        /// redraw all lines
        /// </summary>
        /// <param name="e"></param>
        private void ReDrawAll(PaintEventArgs e)
        {
            foreach (var line in lines)
            {
                line.Draw(e);
            }
        }

        /// <summary>
        /// remove last Line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void remove_Click(object sender, EventArgs e)
        {
            if (lines.Count != 0)
            {
                unre.AddCommand(new CommandLine(lines[lines.Count - 1], "Remove"), true);
                CheckStackUnRe();
                lines.RemoveAt(lines.Count - 1);
            }
            editBox.Invalidate();
        }

        private void clearAll_Click(object sender, EventArgs e)
        {
            lines = new List<Line>();
            editBox.Invalidate();
        }

        /// <summary>
        /// change line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changePosition_Click(object sender, EventArgs e)
        {
            manipulationFlag = true;
            canPaintLine = false;
            isStartCoordinate = true;
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            unre.Undo(lines);
            CheckStackUnRe();
            editBox.Invalidate();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            unre.Redo(lines);
            CheckStackUnRe();
            editBox.Invalidate();
        }
        
        /// <summary>
        /// make enable or disable undo and redo button
        /// </summary>
        private void CheckStackUnRe()
        {
            undoButton.Enabled = !unre.UndoStackIsEmpty(); 
            redoButton.Enabled = !unre.RedoStackIsEmpty();
        }

        private void editBox_Click(object sender, EventArgs e)
        {

        }

        private void SetRedColor(object sender, EventArgs e)
        {
            lineColor = Color.Red;
        }

        private void SetGreenColor(object sender, EventArgs e)
        {
            lineColor = Color.Green;
        }

        private void SetBlackColor(object sender, EventArgs e)
        {
            lineColor = Color.Green;
        }

        private void BlueColorButton(object sender, EventArgs e)
        {
            lineColor = Color.Blue;
        }
    }
}
