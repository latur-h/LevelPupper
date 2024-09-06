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

            builder = new(Path.Combine("js", "general.js"), Path.Combine(@"js", "descriptions elements.js"));

            Init();
        }
        private void Init() => AddClipboardFormatListener(_handle);
        public void Parse()
        {
            try
            {
                if (!_form.Invoke(() => Clipboard.ContainsText(TextDataFormat.Text))) return;

                string text = _form.Invoke(() => Clipboard.GetText(TextDataFormat.UnicodeText));

                if (Regex.IsMatch(text, @"javascript\:"))
                {
                    return;
                }
                else if (RegularExp.isHeader().IsMatch(text))
                {
                    currentText = HeaderJS();
                }
                else if (!RegularExp.isFooter().IsMatch(text))
                {
                    currentText = FooterJS();
                }
                else
                {
                    return;
                }

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
        private string HeaderJS()
        {
            string html = _form.Invoke(() => Clipboard.GetText(TextDataFormat.Html));

            using (Header header = new(html))            
                return builder.Build(JavaScriptBuilder.Script.General, header: header);
        }
        private string FooterJS()
        {
            string html = _form.Invoke(() => Clipboard.GetText(TextDataFormat.Html));
            
            using (Footer footer = new(html, addtionalOptions_Feature: _form.cb_AdditionalOptions.Checked))
                return builder.Build(JavaScriptBuilder.Script.Description, footer: footer);
        }
        public void Dispose() => RemoveClipboardFormatListener(_handle);
    }
}
