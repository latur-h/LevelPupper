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
            cb_AdditionalOptions = new CheckBox();
            label_Version = new Label();
            checkBox_DefaultPosstion = new CheckBox();
            textBox_DefaultPossition = new TextBox();
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
            // cb_AdditionalOptions
            // 
            cb_AdditionalOptions.AutoSize = true;
            cb_AdditionalOptions.Location = new Point(899, 181);
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
            checkBox_DefaultPosstion.Location = new Point(899, 68);
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
            textBox_DefaultPossition.Location = new Point(899, 93);
            textBox_DefaultPossition.Name = "textBox_DefaultPossition";
            textBox_DefaultPossition.Size = new Size(126, 23);
            textBox_DefaultPossition.TabIndex = 5;
            textBox_DefaultPossition.KeyPress += textBox_DefaultPossition_KeyPress;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1054, 558);
            Controls.Add(textBox_DefaultPossition);
            Controls.Add(checkBox_DefaultPosstion);
            Controls.Add(label_Version);
            Controls.Add(cb_AdditionalOptions);
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
            PerformLayout();
        }

        #endregion

        public Button button_Category;
        public RichTextBox rtConsole;
        public CheckBox cb_AdditionalOptions;
        public Label label_Version;
        public CheckBox checkBox_DefaultPosstion;
        public TextBox textBox_DefaultPossition;
    }
}
