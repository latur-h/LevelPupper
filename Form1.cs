using HtmlAgilityPack;
using LevelPupper__Parser.dlls;
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
            parser = new(this, Handle);

            RTConsole.Init(ref rtConsole);
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
    }
}
