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
            } catch { RTConsole.Write("Version control file is not found. This message can be ignored.", Color.Red); }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            parser?.Dispose();
        }
        private void button_Category_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Invoke(() => Clipboard.ContainsText(TextDataFormat.Text))) return;

                string html = Invoke(() => Clipboard.GetText(TextDataFormat.Html));

                HtmlAgilityPack.HtmlDocument doc = new();
                doc.LoadHtml(html);

                string text = _parseHtml(doc.DocumentNode);
                text = Regex.Replace(text, @"H[23]\s-\s", string.Empty);

                Invoke(() => Clipboard.SetText(text, TextDataFormat.UnicodeText));
            }
            catch (Exception ex) 
            {
                RTConsole.Write(ex.Message);
            }
            finally
            {
                GC.Collect();
            }

            string _parseHtml(HtmlNode node)
            {
                StringBuilder result = new StringBuilder();

                foreach (var child in node.ChildNodes)
                {
                    if (child.NodeType == HtmlNodeType.Text)
                    {
                        result.Append(child.InnerText);
                    }

                    if (child.NodeType == HtmlNodeType.Element)
                    {
                        switch (child.Name)
                        {
                            case "h2":
                            case "h3":
                            case "h4":
                            case "ul":
                            case "ol":
                                result.Append($"<{child.Name}>");
                                result.Append(_parseHtml(child));
                                result.Append($"</{child.Name}>");
                                break;
                            case "span":
                                if (child.ParentNode is not null && (child.ParentNode.Name == "li" || child.ParentNode.Name == "p") && child.Attributes["style"].Value.Contains("font-weight:700;"))
                                {
                                    result.Append($"<strong>");
                                    result.Append(_parseHtml(child));
                                    result.Append($"</strong>");
                                }
                                else
                                {
                                    result.Append(_parseHtml(child));
                                }
                                break;
                            case "li":                                
                                result.Append($"<{child.Name}>");
                                result.Append(_parseHtml(child));
                                result.Append($"</{child.Name}>");
                                break;
                            case "p":
                                if (child.ParentNode != null && child.ParentNode.Name == "li")
                                {
                                    result.Append(_parseHtml(child));
                                }
                                else
                                {
                                    result.Append("<p style=\"text-indent: 15px;\">");
                                    result.Append(_parseHtml(child));
                                    result.Append("</p>");
                                }
                                break;
                            default:
                                result.Append(_parseHtml(child));
                                break;
                        }
                    }
                }
                return result.ToString();
            }
        }
    }
}
