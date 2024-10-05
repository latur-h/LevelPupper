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
            button_Execute = new Button();
            comboBox_Game = new ComboBox();
            textBox_Codename = new TextBox();
            label1 = new Label();
            label2 = new Label();
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
            rtConsole.LinkClicked += rtConsole_LinkClicked;
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
            // button_Execute
            // 
            button_Execute.Location = new Point(926, 473);
            button_Execute.Name = "button_Execute";
            button_Execute.Size = new Size(75, 23);
            button_Execute.TabIndex = 8;
            button_Execute.Text = "Execute";
            button_Execute.UseVisualStyleBackColor = true;
            button_Execute.Click += button_Execute_Click;
            // 
            // comboBox_Game
            // 
            comboBox_Game.FormattingEnabled = true;
            comboBox_Game.Items.AddRange(new object[] { "path-of-exile", "wow", "d2", "wow-cataclysm", "season-of-discovery", "apex", "d4", "last-epoch", "the-first-descendant" });
            comboBox_Game.Location = new Point(904, 365);
            comboBox_Game.Name = "comboBox_Game";
            comboBox_Game.Size = new Size(121, 23);
            comboBox_Game.TabIndex = 9;
            // 
            // textBox_Codename
            // 
            textBox_Codename.Location = new Point(904, 427);
            textBox_Codename.Name = "textBox_Codename";
            textBox_Codename.Size = new Size(121, 23);
            textBox_Codename.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(904, 338);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 11;
            label1.Text = "Game";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(904, 400);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 12;
            label2.Text = "Codename";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1054, 558);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox_Codename);
            Controls.Add(comboBox_Game);
            Controls.Add(button_Execute);
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
        private Button button_Execute;
        private ComboBox comboBox_Game;
        private TextBox textBox_Codename;
        private Label label1;
        private Label label2;
    }
}
