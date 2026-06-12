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
            btnReturnLevel = new Button();
            btnPrevLevel = new Button();
            lblStepCount = new Label();
            SuspendLayout();
            // 
            // btnNextLevel
            // 
            btnNextLevel.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNextLevel.ForeColor = Color.Green;
            btnNextLevel.Location = new Point(885, 201);
            btnNextLevel.Name = "btnNextLevel";
            btnNextLevel.Size = new Size(168, 67);
            btnNextLevel.TabIndex = 0;
            btnNextLevel.Text = "Màn tiếp theo";
            btnNextLevel.UseVisualStyleBackColor = true;
            btnNextLevel.Click += btnNextLevel_Click;
            // 
            // btnReturnLevel
            // 
            btnReturnLevel.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReturnLevel.ForeColor = Color.Gold;
            btnReturnLevel.Location = new Point(885, 347);
            btnReturnLevel.Name = "btnReturnLevel";
            btnReturnLevel.Size = new Size(148, 67);
            btnReturnLevel.TabIndex = 1;
            btnReturnLevel.Text = "Chơi lại";
            btnReturnLevel.UseVisualStyleBackColor = true;
            btnReturnLevel.Click += btnReturnLevel_Click;
            // 
            // btnPrevLevel
            // 
            btnPrevLevel.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPrevLevel.ForeColor = Color.Red;
            btnPrevLevel.Location = new Point(885, 274);
            btnPrevLevel.Name = "btnPrevLevel";
            btnPrevLevel.Size = new Size(148, 67);
            btnPrevLevel.TabIndex = 2;
            btnPrevLevel.Text = "Màn trước";
            btnPrevLevel.UseVisualStyleBackColor = true;
            btnPrevLevel.Click += btnPrevLevel_Click;
            // 
            // lblStepCount
            // 
            lblStepCount.AutoSize = true;
            lblStepCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStepCount.ForeColor = Color.FromArgb(0, 0, 192);
            lblStepCount.Location = new Point(885, 159);
            lblStepCount.Name = "lblStepCount";
            lblStepCount.Size = new Size(84, 28);
            lblStepCount.TabIndex = 3;
            lblStepCount.Text = "Bước: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1092, 672);
            Controls.Add(lblStepCount);
            Controls.Add(btnPrevLevel);
            Controls.Add(btnReturnLevel);
            Controls.Add(btnNextLevel);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnNextLevel;
        private Button btnReturnLevel;
        private Button btnPrevLevel;
        private Label lblStepCount;
    }
}
