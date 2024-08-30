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
        private readonly Form _form;
        private readonly IntPtr _handle;

        private readonly JavaScriptBuilder builder;

        private Regex _rewards = new(@"^\bRewards\b", RegexOptions.IgnoreCase);

        private string currentText = string.Empty;

        public static readonly int WM_CLIPBOARDUPDATE = 0x031D;

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        public Parser(Form form, IntPtr handle)
        {
            _form = form;
            _handle = handle;

            builder = new(Path.Combine(@"js", @"general.js"), Path.Combine(@"js", @"descriptions elements.js"));

            Init();
        }
        private void Init() => AddClipboardFormatListener(_handle);
        public void Parse()
        {
            try
            {
                if (!_form.Invoke(() => Clipboard.ContainsText(TextDataFormat.Text))) return;

                string text = _form.Invoke(() => Clipboard.GetText(TextDataFormat.UnicodeText));

                if (_rewards.IsMatch(text))
                {
                    if (currentText == _rewards.Match(text).Value) return;

                    string[] lines = text.Split(new char[] { '\r', '\n' });

                    string rewards = string.Empty;
                    foreach (var i in lines.Where(x => x.Length != 0))
                    {
                        if (_rewards.IsMatch(i)) continue;

                        rewards += $"<div><h3>{i}</div></h3>\r\n";
                    }

                    currentText = rewards;
                }
                else if (Regex.IsMatch(text, @"javascript\:"))
                {
                    return;
                }
                else if (Regex.IsMatch(text, @"Description.*Title.*URL", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                {
                    currentText = HeaderJS();
                }
                else if (!Regex.IsMatch(text, @"(<h2>Requirements<\/h2>)|(<h2>Additional Options<\/h2>)|(<h[23]>Boosting Method[s]?<\/h[23]>)|(<h2>FAQ[s]?<\/h2>)", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                {
                    currentText = FooterJS();
                }
                else
                {
                    GC.Collect();
                    return;
                }

                if (string.IsNullOrEmpty(currentText))
                {
                    GC.Collect();
                    return;
                }

                _form.Invoke(() => Clipboard.SetText(currentText, TextDataFormat.UnicodeText));
            }
            catch
            {
                //RTConsole.Write(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private string HeaderJS()
        {
            string html = _form.Invoke(() => Clipboard.GetText(TextDataFormat.Html));

            using (Header header = new(html))            
                return builder.Build(JavaScriptBuilder.Script.General, header);            
        }
        private string FooterJS()
        {
            string html = _form.Invoke(() => Clipboard.GetText(TextDataFormat.Html));

            using (Footer footer = new(html))
                return builder.Build(JavaScriptBuilder.Script.Description, null, footer);
        }
        public void Dispose() => RemoveClipboardFormatListener(_handle);
    }
}
