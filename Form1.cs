using HtmlAgilityPack;
using LevelPupper__Parser.dlls;
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
            string url = "https://api.levelupper.com/api/v1/services/view/";

            using (HttpClient client = new HttpClient())
            {
                var payload = new
                {
                    game_codename = comboBox_Game.Text,
                    service_codename = textBox_Codename.Text,
                    anon_session_id = "",
                    service_token = ""
                };

                string jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        Product? product = JsonConvert.DeserializeObject<Product>(responseContent);

                        if (product is null) throw new Exception("Invalid data.");

                        StringBuilder rtfText = new StringBuilder(@"{\rtf1\ansi Description elements:\par");

                        foreach (var i in product.service?.description_elements?.Where(x => x.elements?.Count > 0))
                        {
                            rtfText.AppendLine($@"\tab {i.title}:\par");

                            foreach (var element in i.elements)
                            {
                                rtfText.AppendLine($@"\tab\tab {{\field{{\*\fldinst HYPERLINK ""https://api.levelupper.com/admin/game_services/elementofdescription/{element.id}""}}{{\fldrslt {element.title}}}}}\par");
                            }
                        }

                        rtfText.AppendLine(@"\par Value options:\par");

                        foreach (var i in product.options)
                        {
                            if (i.range_gradations?.Count > 0)
                            {
                                rtfText.AppendLine($@"\tab {i.title}:\par");

                                foreach (var element in i.range_gradations)
                                {
                                    rtfText.AppendLine($@"\tab\tab {{\field{{\*\fldinst HYPERLINK ""https://api.levelupper.com/admin/game_services/valueoption/{element.id}""}}{{\fldrslt {element.title}}}}}\par");
                                }
                            }

                            if (i.values_options?.Count > 0)
                            {
                                rtfText.AppendLine($@"\tab {i.title}:\par");

                                foreach (var element in i.values_options)
                                {
                                    rtfText.AppendLine($@"\tab\tab {{\field{{\*\fldinst HYPERLINK ""https://api.levelupper.com/admin/game_services/valueoption/{element.id}""}}{{\fldrslt {element.title}}}}}\par");
                                }
                            }
                        }

                        rtfText.AppendLine("}");

                        rtConsole.Rtf = rtfText.ToString();
                    }
                    else
                    {
                        RTConsole.Write("Request failed with status code: " + response.StatusCode, Color.Red);
                    }
                }
                catch (Exception ex)
                {
                    RTConsole.Write("Error: " + ex.Message, Color.Red);
                }
            }
        }
    }
}
