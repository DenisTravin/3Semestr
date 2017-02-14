namespace GraphiEditor
{
    partial class Editor
    {
        /// <summary>
        /// necessarily constructor var
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// free all resources
        /// </summary>
        /// <param name="disposing">true, if resources must be deleted. Else false</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.editBox = new System.Windows.Forms.PictureBox();
            this.changePosition = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.redoButton = new System.Windows.Forms.Button();
            this.clearAll = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.BlackColorButton = new System.Windows.Forms.Button();
            this.GreenColorButton = new System.Windows.Forms.Button();
            this.RedColorButton = new System.Windows.Forms.Button();
            this.BlColorButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.editBox)).BeginInit();
            this.SuspendLayout();
            // 
            // editBox
            // 
            this.editBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.editBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.editBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.editBox.Location = new System.Drawing.Point(0, 0);
            this.editBox.Margin = new System.Windows.Forms.Padding(2);
            this.editBox.Name = "editBox";
            this.editBox.Size = new System.Drawing.Size(435, 449);
            this.editBox.TabIndex = 0;
            this.editBox.TabStop = false;
            this.editBox.Click += new System.EventHandler(this.editBox_Click);
            this.editBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxPaintHandler);
            this.editBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseClickHandler);
            this.editBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseMoveHandler);
            // 
            // changePosition
            // 
            this.changePosition.Location = new System.Drawing.Point(450, 10);
            this.changePosition.Margin = new System.Windows.Forms.Padding(2);
            this.changePosition.Name = "changePosition";
            this.changePosition.Size = new System.Drawing.Size(96, 35);
            this.changePosition.TabIndex = 1;
            this.changePosition.Text = "Change line position";
            this.changePosition.UseVisualStyleBackColor = true;
            this.changePosition.Click += new System.EventHandler(this.changePosition_Click);
            // 
            // undoButton
            // 
            this.undoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.undoButton.Location = new System.Drawing.Point(450, 61);
            this.undoButton.Margin = new System.Windows.Forms.Padding(2);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(96, 35);
            this.undoButton.TabIndex = 2;
            this.undoButton.Text = "Undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.redoButton.Location = new System.Drawing.Point(450, 113);
            this.redoButton.Margin = new System.Windows.Forms.Padding(2);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(96, 35);
            this.redoButton.TabIndex = 3;
            this.redoButton.Text = "Redo";
            this.redoButton.UseVisualStyleBackColor = true;
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // clearAll
            // 
            this.clearAll.Location = new System.Drawing.Point(450, 162);
            this.clearAll.Margin = new System.Windows.Forms.Padding(2);
            this.clearAll.Name = "clearAll";
            this.clearAll.Size = new System.Drawing.Size(96, 35);
            this.clearAll.TabIndex = 4;
            this.clearAll.Text = "Clear all";
            this.clearAll.UseVisualStyleBackColor = true;
            this.clearAll.Click += new System.EventHandler(this.clearAll_Click);
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(450, 216);
            this.remove.Margin = new System.Windows.Forms.Padding(2);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(96, 35);
            this.remove.TabIndex = 5;
            this.remove.Text = "Remove last line";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // BlackColorButton
            // 
            this.BlackColorButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BlackColorButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BlackColorButton.Location = new System.Drawing.Point(450, 267);
            this.BlackColorButton.Name = "BlackColorButton";
            this.BlackColorButton.Size = new System.Drawing.Size(96, 34);
            this.BlackColorButton.TabIndex = 6;
            this.BlackColorButton.Text = "Black";
            this.BlackColorButton.UseVisualStyleBackColor = false;
            this.BlackColorButton.Click += new System.EventHandler(this.SetBlackColor);
            // 
            // GreenColorButton
            // 
            this.GreenColorButton.BackColor = System.Drawing.Color.Green;
            this.GreenColorButton.Location = new System.Drawing.Point(450, 307);
            this.GreenColorButton.Name = "GreenColorButton";
            this.GreenColorButton.Size = new System.Drawing.Size(96, 35);
            this.GreenColorButton.TabIndex = 7;
            this.GreenColorButton.Text = "Green";
            this.GreenColorButton.UseVisualStyleBackColor = false;
            this.GreenColorButton.Click += new System.EventHandler(this.SetGreenColor);
            // 
            // RedColorButton
            // 
            this.RedColorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RedColorButton.Location = new System.Drawing.Point(450, 348);
            this.RedColorButton.Name = "RedColorButton";
            this.RedColorButton.Size = new System.Drawing.Size(96, 32);
            this.RedColorButton.TabIndex = 8;
            this.RedColorButton.Text = "Red";
            this.RedColorButton.UseVisualStyleBackColor = false;
            this.RedColorButton.Click += new System.EventHandler(this.SetRedColor);
            // 
            // BlColorButton
            // 
            this.BlColorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.BlColorButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BlColorButton.Location = new System.Drawing.Point(450, 386);
            this.BlColorButton.Name = "BlColorButton";
            this.BlColorButton.Size = new System.Drawing.Size(96, 30);
            this.BlColorButton.TabIndex = 9;
            this.BlColorButton.Text = "Blue";
            this.BlColorButton.UseVisualStyleBackColor = false;
            this.BlColorButton.Click += new System.EventHandler(this.BlueColorButton);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 449);
            this.Controls.Add(this.BlColorButton);
            this.Controls.Add(this.RedColorButton);
            this.Controls.Add(this.GreenColorButton);
            this.Controls.Add(this.BlackColorButton);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.clearAll);
            this.Controls.Add(this.redoButton);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.changePosition);
            this.Controls.Add(this.editBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Editor";
            this.Text = "Graph Editor";
            ((System.ComponentModel.ISupportInitialize)(this.editBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox editBox;
        private System.Windows.Forms.Button changePosition;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Button redoButton;
        private System.Windows.Forms.Button clearAll;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button BlackColorButton;
        private System.Windows.Forms.Button GreenColorButton;
        private System.Windows.Forms.Button RedColorButton;
        private System.Windows.Forms.Button BlColorButton;
    }
}

