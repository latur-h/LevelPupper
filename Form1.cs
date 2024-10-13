using HtmlAgilityPack;
using LevelPupper__Parser.dlls;
using LevelPupper__Parser.dlls.API;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LevelPupper__Parser
{
    public partial class Form1 : Form
    {
        private Parser? parser;
        internal API_Pupser? _pupser;

        private Dictionary<string, string> games;

        public Form1()
        {
            InitializeComponent();

            comboBox_Game.FlatStyle = FlatStyle.System;
        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == Parser.WM_CLIPBOARDUPDATE)
                    parser?.Parse();
            }
            catch { }
            finally { }

            base.WndProc(ref m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RTConsole.Init(ref rtConsole);

            try
            {
                if (!Path.Exists(@"data\.env")) throw new Exception();

                Config.LoadConfigs(@"data\.env");
            }
            catch
            {
                foreach (Control control in this.Controls)
                    control.Enabled = false;

                rtConsole.Enabled = true;
                rtConsole.ReadOnly = true;

                RTConsole.Write("Oops! Something went wrong, I couldn`t find an importat files. All functions are dissabled.");

                return;
            }

            parser = new(this, Handle);

            games = new()
            {
                { "Path of Exile", "path-of-exile" },
                { "World of Warcraft", "wow" },
                { "Desctiny 2", "d2" },
                { "WoW Cataclysm", "wow-cataclysm" },
                { "WoW SoD", "season-of-discovery" },
                { "Apex", "apex" },
                { "Diablo IV", "d4" },
                { "Last Epoch", "last-epoch" },
                { "The First Descendant", "the-first-descendant" }
            };

            foreach (var i in games)
                comboBox_Game.Items.Add(i.Key);

            try
            {
                label_Version.Text = $"{File.ReadAllText(@"../../../version.txt")} © Latur";
            }
            catch { RTConsole.Write("Version control file is not found. This message can be ignored.", Color.Red); }


        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            parser?.Dispose();
        }

        private void checkBox_DefaultPosstion_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_DefaultPosstion.Checked)
                textBox_DefaultPossition.Enabled = true;
            else
                textBox_DefaultPossition.Enabled = false;
        }

        private void textBox_DefaultPossition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void rtConsole_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.LinkText,
                UseShellExecute = true
            });
        }

        private void button_Execute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox_Game.Text) || string.IsNullOrEmpty(textBox_Codename.Text))
            {
                if (string.IsNullOrEmpty(comboBox_Game.Text))
                    RTConsole.Write("Error! Choose a game to proceed.", Color.Red);

                if (string.IsNullOrEmpty(textBox_Codename.Text))
                    RTConsole.Write("Error! Write a codename to proceed.", Color.Red);
                return;
            }

            _pupser = new(games[comboBox_Game.Text], textBox_Codename.Text, Config.GetAPI());

            if (!_pupser.Init())
                return;

            rtConsole.Clear();
            rtConsole.Rtf = _pupser.GetServices();
            rtConsole.ReadOnly = false;

            button_Save.Enabled = true;
        }

        private async void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                button_Save.Enabled = false;

                rtConsole.Rtf = await _pupser.Save(rtConsole.Text);
                _pupser = null;

                rtConsole.ReadOnly = true;

                RTConsole.Write("Pip");
                RTConsole.Write("Price change is compelete.", Color.Green);
            }
            catch (Exception ex)
            {
                RTConsole.Write("Pip");
                RTConsole.Write($"Price change is failed with next message: {ex.Message}", Color.Red);

                button_Save.Enabled = true;
            }
        }

        private void comboBox_Game_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_Game_DropDownClosed(object sender, EventArgs e)
        {
            comboBox_Game.SelectionStart = 0;
            comboBox_Game.SelectionLength = 0;

            this.ActiveControl = null;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            rtConsole.Size = new Size(this.Width - 250, this.Height - 70);
        }
    }
}
