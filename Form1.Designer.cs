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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button_Category = new Button();
            rtConsole = new RichTextBox();
            SuspendLayout();
            // 
            // button_Category
            // 
            button_Category.Location = new Point(899, 12);
            button_Category.Name = "button_Category";
            button_Category.Size = new Size(143, 35);
            button_Category.TabIndex = 0;
            button_Category.Text = "Format the Category";
            button_Category.UseVisualStyleBackColor = true;
            button_Category.Click += button_Category_Click;
            // 
            // rtConsole
            // 
            rtConsole.BackColor = Color.Black;
            rtConsole.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtConsole.ForeColor = Color.LightGray;
            rtConsole.Location = new Point(12, 12);
            rtConsole.Name = "rtConsole";
            rtConsole.ReadOnly = true;
            rtConsole.Size = new Size(881, 534);
            rtConsole.TabIndex = 1;
            rtConsole.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1054, 558);
            Controls.Add(rtConsole);
            Controls.Add(button_Category);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(1070, 597);
            MinimumSize = new Size(1070, 597);
            Name = "Form1";
            Text = "Level Pupper";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        public Button button_Category;
        public RichTextBox rtConsole;
    }
}
