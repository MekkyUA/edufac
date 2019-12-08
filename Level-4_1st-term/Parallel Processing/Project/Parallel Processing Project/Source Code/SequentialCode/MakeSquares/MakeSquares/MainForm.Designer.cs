namespace MakeSquares
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ResultsTextBox = new System.Windows.Forms.TextBox();
            this.solveBtn = new System.Windows.Forms.Button();
            this.ShapesTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ResultsTextBox
            // 
            this.ResultsTextBox.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ResultsTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultsTextBox.ForeColor = System.Drawing.Color.White;
            this.ResultsTextBox.Location = new System.Drawing.Point(12, 12);
            this.ResultsTextBox.Multiline = true;
            this.ResultsTextBox.Name = "ResultsTextBox";
            this.ResultsTextBox.ReadOnly = true;
            this.ResultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultsTextBox.Size = new System.Drawing.Size(648, 398);
            this.ResultsTextBox.TabIndex = 1;
            // 
            // solveBtn
            // 
            this.solveBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solveBtn.Location = new System.Drawing.Point(666, 387);
            this.solveBtn.Name = "solveBtn";
            this.solveBtn.Size = new System.Drawing.Size(317, 23);
            this.solveBtn.TabIndex = 2;
            this.solveBtn.Text = "Solve";
            this.solveBtn.UseVisualStyleBackColor = true;
            this.solveBtn.Click += new System.EventHandler(this.solveBtn_Click);
            // 
            // ShapesTextBox
            // 
            this.ShapesTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.ShapesTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShapesTextBox.Location = new System.Drawing.Point(666, 12);
            this.ShapesTextBox.Multiline = true;
            this.ShapesTextBox.Name = "ShapesTextBox";
            this.ShapesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ShapesTextBox.Size = new System.Drawing.Size(317, 369);
            this.ShapesTextBox.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 420);
            this.Controls.Add(this.ShapesTextBox);
            this.Controls.Add(this.solveBtn);
            this.Controls.Add(this.ResultsTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Make Squares";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ResultsTextBox;
        private System.Windows.Forms.Button solveBtn;
        private System.Windows.Forms.TextBox ShapesTextBox;
    }
}

