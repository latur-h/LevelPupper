namespace LevelPupper__Parser
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
            button_Category = new Button();
            SuspendLayout();
            // 
            // button_Category
            // 
            button_Category.Location = new Point(216, 87);
            button_Category.Name = "button_Category";
            button_Category.Size = new Size(193, 71);
            button_Category.TabIndex = 0;
            button_Category.Text = "Format the Category";
            button_Category.UseVisualStyleBackColor = true;
            button_Category.Click += button_Category_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(644, 285);
            Controls.Add(button_Category);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        public Button button_Category;
    }
}
