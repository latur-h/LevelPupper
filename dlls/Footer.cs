using HtmlAgilityPack;
using System;
using System.Collections.Generic;
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

        public Dictionary<string, string>? items;

        public Dictionary<string, string>? boostingMethods;
        public Dictionary<string, string>? faqs;

        private readonly HtmlAgilityPack.HtmlDocument doc;

        public Footer(string html) 
        {
            HtmlAgilityPack.HtmlDocument doc = new();
            doc.LoadHtml(html);

            this.doc = doc;

            Init();
        }
        private void Init()
        {
            try
            {
                string text = ParseHtml(doc.DocumentNode);

                text = Regex.Replace(text, @"H[23]\s-\s", string.Empty, RegexOptions.IgnoreCase);

                if (!Regex.IsMatch(text, @"<h2>Requirements<\/h2>") || !Regex.IsMatch(text, @"<h2>\bAdditional Options\b<\/h2>") || !(Regex.IsMatch(text, @"<h2>\bBoosting Method\b<\/h2>") || Regex.IsMatch(text, @"\bBoosting Methods\b")) || !(Regex.IsMatch(text, @"<h2>FAQ<\/h2>") || Regex.IsMatch(text, @"<h2>FAQs<\/h2>")))
                {
                    throw new Exception("Incorrect tags");
                }                

                items = ParseHtmlBlocks(text);

                _requirements = items["Requirements"];
                _additionalOptions = items["Additional Options"];

                _aboutTitle = items.ElementAt(items.Count - 2).Key;
                _aboutText = items.ElementAt(items.Count - 2).Value;

                boostingMethods = boostingMethod_Parse(items["Boosting Method"]);
                faqs = FAQs_Parse(items["FAQs"]);

                MessageBox.Show("Match the Footer!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
        private Dictionary<string, string> FAQs_Parse(string? html)
        {
            if (html is null)
                throw new Exception("Input in null");

            html = Regex.Replace(html, @"\[link\!\]", string.Empty, RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"H[23]\s-\s", string.Empty, RegexOptions.IgnoreCase);

            Regex regex = new Regex(@"<h3>(.*?)<\/h3>\s*<p>(.*?)<\/p>", RegexOptions.Singleline);
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
        private Dictionary<string, string> boostingMethod_Parse(string? html)
        {
            if (html is null)
                throw new Exception("Input string is null");

            html = Regex.Replace(html, @"<(\/?)strong>", string.Empty);

            Regex regex = new Regex(@"<li>(.*?)<\/li>", RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(html);

            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (Match match in matches)
            {
                string line = match.Groups[1].Value.Trim();
                string[] parts = line.Split(new[] { ':', '–' }, 2); // Split by the first colon

                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    result[key] = value;
                }
            }

            return result;
        }
        private Dictionary<string, string> ParseHtmlBlocks(string input)
        {
            var predefinedKeys = new List<string> { "Requirements", "Additional Options", "Boosting Method" };
            var blocks = new Dictionary<string, string>();

            string pattern = @"<h2>(?<title>.*?)<\/h2>(?<content>.*?)(?=<h2>|$)";
            var matches = Regex.Matches(input, pattern, RegexOptions.Singleline);

            int keyIndex = 0;

            foreach (Match match in matches)
            {
                if (keyIndex == 3)
                {
                    blocks[match.Groups["title"].Value] = Regex.Replace(match.Groups["content"].Value.Trim(), @"\[link\!\]", string.Empty); 
                    keyIndex++;
                    continue;
                }
                else if (keyIndex == 4)
                {
                    blocks["FAQs"] = match.Groups["content"].Value.Trim();
                    break;
                }

                string key = predefinedKeys[keyIndex];
                string content = match.Groups["content"].Value.Trim();
                blocks[key] = content;

                keyIndex++;
            }

            return blocks;
        }

        public void Dispose()
        {
            _requirements = string.Empty;
            _additionalOptions = string.Empty;

            _aboutTitle = string.Empty;
            _aboutText = string.Empty;

            items?.Clear();
            items = null;

            boostingMethods?.Clear();
            boostingMethods = null;

            faqs?.Clear();
            faqs = null;
        }
    }
}
