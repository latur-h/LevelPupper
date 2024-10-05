using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace LevelPupper__Parser.dlls
{
    internal class Parser : IDisposable
    {
        private readonly Form1 _form;
        private readonly IntPtr _handle;

        private readonly JavaScriptBuilder builder;

        private string currentText = string.Empty;

        public static readonly int WM_CLIPBOARDUPDATE = 0x031D;

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        public Parser(Form1 form, IntPtr handle)
        {
            _form = form;
            _handle = handle;

            builder = new(Path.Combine("js", "general.js"), Path.Combine(@"js", "descriptions elements.js"), Path.Combine(@"js", "category.js"));

            Init();
        }
        private void Init() => AddClipboardFormatListener(_handle);
        public void Parse()
        {
            try
            {
                if (!_form.Invoke(() => Clipboard.ContainsText(TextDataFormat.Text))) return;

                string text = _form.Invoke(() => Clipboard.GetText(TextDataFormat.Html));

                HtmlAgilityPack.HtmlDocument doc = new();
                doc.LoadHtml(text);

                text = ParseHtml(doc.DocumentNode);
                text = HttpUtility.HtmlDecode(text);

                //RTConsole.Write(text);

                if (Regex.IsMatch(text, @"insertStaticText"))
                    return;
                else if (_form.cb_Category.Checked)
                    currentText = CategoryJS();
                else if (RegularExp.isHeader().IsMatch(text))
                    currentText = HeaderJS();
                else if (RegularExp.isFooter().IsMatch(text))
                    currentText = FooterJS();
                else
                    return;

                if (string.IsNullOrEmpty(currentText))
                {
                    return;
                }

                _form.Invoke(() => Clipboard.SetText(currentText, TextDataFormat.UnicodeText));
            }
            catch //(Exception ex)
            {
                //RTConsole.Write(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private string ParseHtml(HtmlNode node)
        {
            StringBuilder result = new StringBuilder();

            foreach (var child in node.ChildNodes)
            {
                if (child.NodeType == HtmlNodeType.Text)
                    result.Append(child.InnerText);

                if (child.NodeType == HtmlNodeType.Element)
                    switch (child.Name)
                    {
                        case "h1":
                        case "h2":
                        case "h3":
                        case "td":
                        case "p":
                            result.Append($"<{child.Name}>");
                            result.Append(ParseHtml(child));
                            result.Append($"</{child.Name}>");
                            break;
                        default:
                            result.Append(ParseHtml(child));
                            break;
                    }
            }

            return result.ToString();
        }
        private string HeaderJS()
        {
            string html = _form.Invoke(() => Clipboard.GetText(TextDataFormat.Html));

            using (Header header = new(html, defaultPossition: GetDefaultPossition()))
                return builder.Build(JavaScriptBuilder.Script.General, header: header);
        }
        private string? GetDefaultPossition()
        {
            if (!_form.checkBox_DefaultPosstion.Checked || _form.textBox_DefaultPossition.Text.Length == 0)
                return null;

            return _form.textBox_DefaultPossition.Text;
        }
        private string FooterJS()
        {
            string html = _form.Invoke(() => Clipboard.GetText(TextDataFormat.Html));
            
            using (Footer footer = new(html, addtionalOptions_Feature: _form.cb_AdditionalOptions.Checked))
                return builder.Build(JavaScriptBuilder.Script.Description, footer: footer, isAboutNullifier: _form.cb_Nullifier.Checked);
        }
        private string CategoryJS()
        {
            string html = _form.Invoke(() => Clipboard.GetText(TextDataFormat.Html));

            using (Ccategory category = new(html))
                return builder.Build(JavaScriptBuilder.Script.Category, category: category);
        }
        public void Dispose() => RemoveClipboardFormatListener(_handle);
    }
}
