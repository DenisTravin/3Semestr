namespace GraphicsEditorNamespace
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class GraphicsEditor : Form
    {
        /// <summary>
        /// true, if we add new line at this moment, else - false
        /// </summary>
        private bool paintFlag = false;

        /// <summary>
        /// true, if we add remove line at this moment, else - false
        /// </summary>
        private bool removalFlag = false;

        /// <summary>
        /// true, if we already choose start point, else - false
        /// </summary>
        private bool isStartPointChoosen = false;

        /// <summary>
        /// true, if we manipulate line at this moment, else - false
        /// </summary>
        private bool manipulationFlag = false;

        /// <summary>
        /// true, if line end dragged, else - false
        /// </summary>
        private bool isLineEndDragged = false;

        private PointF currentMousePosition;
        private PointF lastHandledClick;
        private PointF notDraggedLineEnd;
        private GraphicsEditorLogic graphicsEditorLogic = new GraphicsEditorLogic();
        UndoRedoHandler undo = new UndoRedoHandler();
        UndoRedoHandler redo = new UndoRedoHandler();

        /// <summary>
        /// Initialize form
        /// </summary>
        public GraphicsEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Deactivate all buttons
        /// </summary>
        private void DeactivateAllButtons()
        {
            buttonsTable.Enabled = false;
        }

        /// <summary>
        /// Activate all buttons
        /// </summary>
        private void ActivateAllButtons()
        {
            buttonsTable.Enabled = true;
        }

        /// <summary>
        /// button click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClickHandler(object sender, EventArgs e)
        {
            Button button = sender as Button;
            switch (button.Text)
            {
                case ("add"):
                    DeactivateAllButtons();
                    paintFlag = true;
                    isStartPointChoosen = false;
                    break;
                case ("remove"):
                    DeactivateAllButtons();
                    removalFlag = true;
                    break;
                case ("undo"):
                    if (!undo.IsEmpty())
                    {
                        updateRedo();
                        graphicsEditorLogic.SetState(undo.GetState());
                        pictureBox.Invalidate();
                    }
                    break;
                case ("redo"):
                    if (!redo.IsEmpty())
                    {
                        updateUndo();
                        graphicsEditorLogic.SetState(redo.GetState());
                        pictureBox.Invalidate();
                    }
                    break;
                case ("manipulate"):
                    DeactivateAllButtons();
                    manipulationFlag = true;
                    break;
            }
        }

        /// <summary>
        /// picture box paint handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxPaintHandler(object sender, PaintEventArgs e)
        {
            graphicsEditorLogic.Draw(e);

            if (paintFlag && isStartPointChoosen)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 3), lastHandledClick, currentMousePosition);    
            }
            else if (manipulationFlag && isLineEndDragged)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 3), notDraggedLineEnd, currentMousePosition);
            }
        }

        /// <summary>
        /// picture box mouse move handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMouseMoveHandler(object sender, MouseEventArgs e)
        {
            currentMousePosition.X = e.X;
            currentMousePosition.Y = e.Y;
            if (paintFlag && isStartPointChoosen || (manipulationFlag && isLineEndDragged))
            {
                pictureBox.Invalidate();
            }
        }

        /// <summary>
        /// mouse click on picture box handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMouseClickHandler(object sender, MouseEventArgs e)
        {
            var prevClick = lastHandledClick;
            lastHandledClick.X = e.X;
            lastHandledClick.Y = e.Y;

            if (paintFlag && isStartPointChoosen)
            {
                AddLine(prevClick, lastHandledClick);
            }
            else if (paintFlag && !isStartPointChoosen)
            {
                isStartPointChoosen = true;
            }
            else if (removalFlag)
            {
                RemoveLine(lastHandledClick);
            }
            else if (manipulationFlag && !isLineEndDragged)
            {
                StartManipulation();
            }
            else if (manipulationFlag && isLineEndDragged)
            {
                AddManipulatedLine();
            }
        }

        /// <summary>
        /// undo state updater
        /// </summary>
        private void updateUndo()
        {
            undo.AddState(graphicsEditorLogic.GetState());
        }

        /// <summary>
        /// redo state updater
        /// </summary>
        private void updateRedo()
        {
            redo.AddState(graphicsEditorLogic.GetState());
        }

        /// <summary>
        /// add new line
        /// </summary>
        /// <param name="prevClick"></param>
        /// <param name="lastHandledClick"></param>
        private void AddLine(PointF prevClick, PointF lastHandledClick)
        {
            updateUndo();
            graphicsEditorLogic.AddLine(new Line(prevClick, lastHandledClick));
            paintFlag = false;
            ActivateAllButtons();
        }

        /// <summary>
        /// remove line
        /// </summary>
        /// <param name="lastHandledClick"></param>
        private void RemoveLine(PointF lastHandledClick)
        {
            updateUndo();
            if (!graphicsEditorLogic.RemoveLine(lastHandledClick))
            {
                undo.GetState();
            }
            removalFlag = false;
            ActivateAllButtons();
            pictureBox.Invalidate();
        }

        /// <summary>
        /// add manipulated line
        /// </summary>
        private void AddManipulatedLine()
        {
            manipulationFlag = false;
            isLineEndDragged = false;
            graphicsEditorLogic.AddLine(new Line(notDraggedLineEnd, lastHandledClick));
            ActivateAllButtons();
        }

        /// <summary>
        /// start the line manipulation
        /// </summary>
        private void StartManipulation()
        {
            updateUndo();
            notDraggedLineEnd = graphicsEditorLogic.FindNotDraggedLineEnd(lastHandledClick);
            if (!notDraggedLineEnd.IsEmpty)
            {
                isLineEndDragged = true;
            }
            else
            {
                undo.GetState();
            }
        }
    }
}