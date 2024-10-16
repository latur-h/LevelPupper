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
            button_Save = new Button();
            panel_Options = new Panel();
            cb_Suppress = new CheckBox();
            panel_Options.SuspendLayout();
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
            rtConsole.Size = new Size(880, 530);
            rtConsole.TabIndex = 1;
            rtConsole.Text = "";
            rtConsole.LinkClicked += rtConsole_LinkClicked;
            // 
            // cb_AdditionalOptions
            // 
            cb_AdditionalOptions.AutoSize = true;
            cb_AdditionalOptions.Location = new Point(24, 140);
            cb_AdditionalOptions.Name = "cb_AdditionalOptions";
            cb_AdditionalOptions.Size = new Size(126, 19);
            cb_AdditionalOptions.TabIndex = 2;
            cb_AdditionalOptions.Text = "Additional Options";
            cb_AdditionalOptions.UseVisualStyleBackColor = true;
            // 
            // label_Version
            // 
            label_Version.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label_Version.AutoSize = true;
            label_Version.Location = new Point(1028, 537);
            label_Version.Name = "label_Version";
            label_Version.Size = new Size(0, 15);
            label_Version.TabIndex = 3;
            // 
            // checkBox_DefaultPosstion
            // 
            checkBox_DefaultPosstion.AutoSize = true;
            checkBox_DefaultPosstion.Location = new Point(24, 38);
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
            textBox_DefaultPossition.Location = new Point(24, 63);
            textBox_DefaultPossition.Name = "textBox_DefaultPossition";
            textBox_DefaultPossition.Size = new Size(152, 23);
            textBox_DefaultPossition.TabIndex = 5;
            textBox_DefaultPossition.KeyPress += textBox_DefaultPossition_KeyPress;
            // 
            // cb_Category
            // 
            cb_Category.AutoSize = true;
            cb_Category.Location = new Point(24, 13);
            cb_Category.Name = "cb_Category";
            cb_Category.Size = new Size(108, 19);
            cb_Category.TabIndex = 6;
            cb_Category.Text = "Category Mode";
            cb_Category.UseVisualStyleBackColor = true;
            // 
            // cb_Nullifier
            // 
            cb_Nullifier.AutoSize = true;
            cb_Nullifier.Location = new Point(24, 115);
            cb_Nullifier.Name = "cb_Nullifier";
            cb_Nullifier.Size = new Size(102, 19);
            cb_Nullifier.TabIndex = 7;
            cb_Nullifier.Text = "About nullifier";
            cb_Nullifier.UseVisualStyleBackColor = true;
            // 
            // button_Execute
            // 
            button_Execute.Location = new Point(46, 319);
            button_Execute.Name = "button_Execute";
            button_Execute.Size = new Size(104, 29);
            button_Execute.TabIndex = 8;
            button_Execute.Text = "Execute";
            button_Execute.UseVisualStyleBackColor = true;
            button_Execute.Click += button_Execute_Click;
            // 
            // comboBox_Game
            // 
            comboBox_Game.FormattingEnabled = true;
            comboBox_Game.Location = new Point(24, 215);
            comboBox_Game.Name = "comboBox_Game";
            comboBox_Game.Size = new Size(152, 23);
            comboBox_Game.TabIndex = 9;
            comboBox_Game.DropDownClosed += comboBox_Game_DropDownClosed;
            comboBox_Game.KeyPress += comboBox_Game_KeyPress;
            // 
            // textBox_Codename
            // 
            textBox_Codename.Location = new Point(24, 280);
            textBox_Codename.Name = "textBox_Codename";
            textBox_Codename.Size = new Size(152, 23);
            textBox_Codename.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 188);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 11;
            label1.Text = "Game";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 253);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 12;
            label2.Text = "Codename";
            // 
            // button_Save
            // 
            button_Save.Enabled = false;
            button_Save.Location = new Point(46, 363);
            button_Save.Name = "button_Save";
            button_Save.Size = new Size(104, 29);
            button_Save.TabIndex = 13;
            button_Save.Text = "Save";
            button_Save.UseVisualStyleBackColor = true;
            button_Save.Click += button_Save_Click;
            // 
            // panel_Options
            // 
            panel_Options.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel_Options.Controls.Add(cb_Suppress);
            panel_Options.Controls.Add(cb_Category);
            panel_Options.Controls.Add(button_Save);
            panel_Options.Controls.Add(button_Execute);
            panel_Options.Controls.Add(checkBox_DefaultPosstion);
            panel_Options.Controls.Add(label2);
            panel_Options.Controls.Add(textBox_DefaultPossition);
            panel_Options.Controls.Add(textBox_Codename);
            panel_Options.Controls.Add(label1);
            panel_Options.Controls.Add(cb_Nullifier);
            panel_Options.Controls.Add(cb_AdditionalOptions);
            panel_Options.Controls.Add(comboBox_Game);
            panel_Options.Location = new Point(902, 12);
            panel_Options.Name = "panel_Options";
            panel_Options.Size = new Size(200, 530);
            panel_Options.TabIndex = 14;
            // 
            // cb_Suppress
            // 
            cb_Suppress.AutoSize = true;
            cb_Suppress.Location = new Point(24, 444);
            cb_Suppress.Name = "cb_Suppress";
            cb_Suppress.Size = new Size(73, 19);
            cb_Suppress.TabIndex = 14;
            cb_Suppress.Text = "Suppress";
            cb_Suppress.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1114, 561);
            Controls.Add(panel_Options);
            Controls.Add(label_Version);
            Controls.Add(rtConsole);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(3840, 2160);
            MinimumSize = new Size(1130, 600);
            Name = "Form1";
            Text = "Level Pupper";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            SizeChanged += Form1_SizeChanged;
            panel_Options.ResumeLayout(false);
            panel_Options.PerformLayout();
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
        private Button button_Save;
        public Panel panel_Options;
        public CheckBox cb_Suppress;
    }
}
