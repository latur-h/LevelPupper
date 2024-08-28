using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LevelPupper__Parser.dlls
{
    internal class Footer : IDisposable
    {
        public string? _requirements;
        public string? _additionalOptions;

        public string? _aboutTitle;
        public string? _aboutText;

        public Dictionary<string, string>? boostingMethods;
        public Dictionary<string, string>? faqs;

        private readonly HtmlAgilityPack.HtmlDocument doc;

        public Footer(string html) 
        {
            HtmlAgilityPack.HtmlDocument doc = new();
            doc.LoadHtml(html);

            this.doc = doc;

            if (!Init()) throw new Exception("Incorrect tags");
        }
        private bool Init()
        {
            string text = ParseHtml(doc.DocumentNode);

            text = Regex.Replace(text, @"H[23]\s-\s", string.Empty, RegexOptions.IgnoreCase);

            try
            {
                if (!Regex.IsMatch(text, @"(<h2>Requirements<\/h2>)|(<h2>Additional Options<\/h2>)|(<h[23]>Boosting Method[s]?<\/h[23]>)|(<h2>FAQ[s]?<\/h2>)", RegexOptions.Singleline | RegexOptions.IgnoreCase))                
                    throw new Exception("Incorrect tags!");
            }
            catch (Exception e) { RTConsole.Write(e.Message + "\n", Color.Red); return false; }

            RTConsole.Write("Start footer parsing...");

            try
            {
                _requirements = GetRequirements(ref text);

                _requirements = _requirements.Replace("\"", "\\\"");
                _requirements = Regex.Replace(_requirements, @"\[link\!\]", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                RTConsole.Write("Requirements have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                _additionalOptions = GetAdditional_Options(ref text);

                _additionalOptions = _additionalOptions.Replace("\"", "\\\"");
                _additionalOptions = Regex.Replace(_additionalOptions, @"\[link\!\]", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                RTConsole.Write("Additional Options have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                string _boostingMethods = GetBoosting_Methods(ref text);

                _boostingMethods = _boostingMethods.Replace("\"", "\\\"");
                _boostingMethods = Regex.Replace(_boostingMethods, @"\[link\!\]", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                boostingMethods = boostingMethod_Parse(_boostingMethods);

                RTConsole.Write("Boosting Methods have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                var about = GetAbout(ref text);

                _aboutTitle = about.Item1.Replace("\"", "\\\"");
                _aboutText = about.Item2.Replace("\"", "\\\"");

                _aboutTitle = Regex.Replace(_aboutTitle, @"\[link\!\]", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                _aboutText = Regex.Replace(_aboutText, @"\[link\!\]", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                RTConsole.Write("About have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                string _faqs = GetFAQs(ref text);

                _faqs = _faqs.Replace("\"", "\\\"");

                faqs = FAQs_Parse(_faqs);

                RTConsole.Write("FAQs have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            RTConsole.Write("Footer parse is complete.\n");

            return true;
        }
        private string ParseHtml(HtmlNode node)
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
                        case "ul":
                        case "ol":
                            result.Append($"<{child.Name}>");
                            result.Append(ParseHtml(child));
                            result.Append($"</{child.Name}>");
                            break;
                        case "span":
                            if (child.ParentNode is not null && (child.ParentNode.Name == "li" || child.ParentNode.Name == "p") && child.Attributes["style"].Value.Contains("font-weight:700;"))
                            {
                                result.Append($"<strong>");
                                result.Append(ParseHtml(child));
                                result.Append($"</strong>");
                            }
                            else
                            {
                                result.Append(ParseHtml(child));
                            }
                            break;
                        case "li":
                            result.Append($"<{child.Name}>");
                            result.Append(ParseHtml(child));
                            result.Append($"</{child.Name}>");
                            break;
                        case "p":
                            if (child.ParentNode != null && child.ParentNode.Name == "li")
                            {
                                result.Append(ParseHtml(child));
                            }
                            else
                            {
                                result.Append("<p>");
                                result.Append(ParseHtml(child));
                                result.Append("</p>");
                            }
                            break;
                        default:
                            result.Append(ParseHtml(child));
                            break;
                    }
                }
            }

            return result.ToString();
        }
        private string GetRequirements(ref string input)
        {
            if (!Regex.IsMatch(input, @"<h2>Requirements<\/h2>(.*?)(?=<h2>|<h3>)", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                throw new Exception("Requirements was not found. Check the name or structure if this a mistake. Block is ignored.");

            return Regex.Match(input, @"<h2>Requirements<\/h2>(.*?)(?=<h2>|<h3>)", RegexOptions.Singleline | RegexOptions.IgnoreCase).Groups[1].Value;
        }
        private string GetAdditional_Options(ref string input)
        {
            if (!Regex.IsMatch(input, @"<h2>Additional Options<\/h2>(.*?)(?=<h2>|<h3>)", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                throw new Exception("Additional Options was not found. Check the name or structure if this a mistake. Block is ignored.");

            return Regex.Match(input, @"<h2>Additional Options<\/h2>(.*?)(?=<h2>|<h3>)", RegexOptions.Singleline | RegexOptions.IgnoreCase).Groups[1].Value;
        }
        private string GetBoosting_Methods(ref string input)
        {
            if (!Regex.IsMatch(input, @"<h[23]>Boosting Method[s]?<\/h[23]>(.*?)(?=<h2>|<h3>)", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                throw new Exception("Boosting Methods was not found. Check the name or structure if this a mistake. Block is ignored.");

            return Regex.Match(input, @"<h[23]>Boosting Method[s]?<\/h[23]>(.*?)(?=<h2>|<h3>)", RegexOptions.Singleline | RegexOptions.IgnoreCase).Groups[1].Value;
        }
        private Tuple<string, string> GetAbout(ref string input)
        {
            if (!Regex.IsMatch(input, @"<h2>About .*?<\/h2>(.*?)(?=<h2>|<h3>)", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                throw new Exception("About was not found. Check the name or structure if this a mistake. Block is ignored.");

            var about = Regex.Matches(input, @"<h2>(About .*?)<\/h2>(.*?)(?=<h2>|<h3>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            return new Tuple<string, string>(about[0].Groups[1].Value.Trim(), about[0].Groups[2].Value.Trim());
        }
        private string GetFAQs(ref string input)
        {
            if (!Regex.IsMatch(input, @"<h2>FAQ[s]?<\/h2>(.*?)$", RegexOptions.Singleline | RegexOptions.IgnoreCase))
            {
                throw new Exception("FAQs was not found. Check the name or structure if this a mistake. Block is ignored.");
            }

            return Regex.Match(input, @"<h2>FAQ[s]?<\/h2>(.*?)$", RegexOptions.Singleline | RegexOptions.IgnoreCase).Groups[1].Value;
        }
        private Dictionary<string, string> boostingMethod_Parse(string html)
        {
            html = Regex.Replace(html, @"<(\/?)strong>", string.Empty);

            Regex regex = new Regex(@"<li>(.*?)<\/li>", RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(html);

            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (Match match in matches)
            {
                string line = match.Groups[1].Value.Trim();
                string[] parts = line.Split(new[] { ':', '–' }, 2);

                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    result[key] = value;
                }
            }

            return result;
        }
        private Dictionary<string, string> FAQs_Parse(string html)
        {
            html = Regex.Replace(html, @"\[link\!\]", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            html = Regex.Replace(html, @"H[23]\s-\s", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            Regex regex = new Regex(@"<h3>(.*?)<\/h3>\s*<p>(.*?)<\/p>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(html);

            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (Match match in matches)
            {
                string key = match.Groups[1].Value.Trim();
                string value = match.Groups[2].Value.Trim();

                if (!result.ContainsKey(key))
                {
                    result[key] = value;
                }
            }

            return result;
        }
        public void Dispose()
        {
            _requirements = string.Empty;
            _additionalOptions = string.Empty;

            _aboutTitle = string.Empty;
            _aboutText = string.Empty;

            boostingMethods?.Clear();
            boostingMethods = null;

            faqs?.Clear();
            faqs = null;
        }
    }
}
