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
            rtConsole = new RichTextBox();
            cb_AdditionalOptions = new CheckBox();
            label_Version = new Label();
            checkBox_DefaultPosstion = new CheckBox();
            textBox_DefaultPossition = new TextBox();
            cb_Category = new CheckBox();
            cb_Nullifier = new CheckBox();
            SuspendLayout();
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
            // cb_AdditionalOptions
            // 
            cb_AdditionalOptions.AutoSize = true;
            cb_AdditionalOptions.Location = new Point(899, 261);
            cb_AdditionalOptions.Name = "cb_AdditionalOptions";
            cb_AdditionalOptions.Size = new Size(126, 19);
            cb_AdditionalOptions.TabIndex = 2;
            cb_AdditionalOptions.Text = "Additional Options";
            cb_AdditionalOptions.UseVisualStyleBackColor = true;
            // 
            // label_Version
            // 
            label_Version.AutoSize = true;
            label_Version.Location = new Point(1004, 534);
            label_Version.Name = "label_Version";
            label_Version.Size = new Size(0, 15);
            label_Version.TabIndex = 3;
            // 
            // checkBox_DefaultPosstion
            // 
            checkBox_DefaultPosstion.AutoSize = true;
            checkBox_DefaultPosstion.Location = new Point(899, 54);
            checkBox_DefaultPosstion.Name = "checkBox_DefaultPosstion";
            checkBox_DefaultPosstion.Size = new Size(115, 19);
            checkBox_DefaultPosstion.TabIndex = 4;
            checkBox_DefaultPosstion.Text = "Default possition";
            checkBox_DefaultPosstion.UseVisualStyleBackColor = true;
            checkBox_DefaultPosstion.CheckedChanged += checkBox_DefaultPosstion_CheckedChanged;
            // 
            // textBox_DefaultPossition
            // 
            textBox_DefaultPossition.Enabled = false;
            textBox_DefaultPossition.Location = new Point(899, 79);
            textBox_DefaultPossition.Name = "textBox_DefaultPossition";
            textBox_DefaultPossition.Size = new Size(126, 23);
            textBox_DefaultPossition.TabIndex = 5;
            textBox_DefaultPossition.KeyPress += textBox_DefaultPossition_KeyPress;
            // 
            // cb_Category
            // 
            cb_Category.AutoSize = true;
            cb_Category.Location = new Point(899, 18);
            cb_Category.Name = "cb_Category";
            cb_Category.Size = new Size(108, 19);
            cb_Category.TabIndex = 6;
            cb_Category.Text = "Category Mode";
            cb_Category.UseVisualStyleBackColor = true;
            // 
            // cb_Nullifier
            // 
            cb_Nullifier.AutoSize = true;
            cb_Nullifier.Location = new Point(899, 130);
            cb_Nullifier.Name = "cb_Nullifier";
            cb_Nullifier.Size = new Size(102, 19);
            cb_Nullifier.TabIndex = 7;
            cb_Nullifier.Text = "About nullifier";
            cb_Nullifier.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1054, 558);
            Controls.Add(cb_Nullifier);
            Controls.Add(cb_Category);
            Controls.Add(textBox_DefaultPossition);
            Controls.Add(checkBox_DefaultPosstion);
            Controls.Add(label_Version);
            Controls.Add(cb_AdditionalOptions);
            Controls.Add(rtConsole);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(1070, 597);
            MinimumSize = new Size(1070, 597);
            Name = "Form1";
            Text = "Level Pupper";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public RichTextBox rtConsole;
        public CheckBox cb_AdditionalOptions;
        public Label label_Version;
        public CheckBox checkBox_DefaultPosstion;
        public TextBox textBox_DefaultPossition;
        public CheckBox cb_Category;
        public CheckBox cb_Nullifier;
    }
}
