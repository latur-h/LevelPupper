using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelPupper__Parser.dlls
{
    static class RTConsole
    {
        static RichTextBox? console;

        public static void Init(ref RichTextBox rt) => console = rt;
        public static void Write(string text, Color? color = null)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            console.SelectionColor = (Color)(color is null ? Color.LightGray : color);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            console?.AppendText($"{DateTime.Now.ToShortTimeString()} | {text}\r");

            console?.ScrollToCaret();
        }
    }
}