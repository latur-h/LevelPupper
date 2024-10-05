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

namespace LevelPupper__Parser
{
    public partial class Form1 : Form
    {
        private Parser? parser;
        private API_Pupser? _pupser;

        public Form1()
        {
            InitializeComponent();
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

                RTConsole.Write("Oops! Something went wrong, I couldn`t find an importat files. System destruction is proceed...");
            }

            parser = new(this, Handle);

            try
            {
                label_Version.Text = $"{File.ReadAllText(@"../../../version.txt")}";
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

        private async void button_Execute_Click(object sender, EventArgs e)
        {
            _pupser = new(comboBox_Game.Text, textBox_Codename.Text, Config.GetAPI());

            rtConsole.Rtf = _pupser.GetServices();

            button_Save.Enabled = true;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            _pupser.Save(rtConsole.Text);

            button_Save.Enabled = false;
        }
    }
}
