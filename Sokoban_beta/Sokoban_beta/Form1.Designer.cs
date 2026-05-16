namespace Sokoban_beta
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnNextLevel = new Button();            
            SuspendLayout();
            // 
            // btnNextLevel
            // 
            btnNextLevel.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNextLevel.Location = new Point(886, 230);
            btnNextLevel.Name = "btnNextLevel";
            btnNextLevel.Size = new Size(148, 67);
            btnNextLevel.TabIndex = 0;
            btnNextLevel.Text = "Next level";
            btnNextLevel.UseVisualStyleBackColor = true;
            btnNextLevel.Click += btnNextLevel_Click;
            // 
            
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1092, 672);
            
            Controls.Add(btnNextLevel);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnNextLevel;
        
    }
}
