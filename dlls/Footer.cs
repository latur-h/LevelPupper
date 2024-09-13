using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

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

        private readonly bool AdditionalOptions_Feature;

        public Footer(string html, bool addtionalOptions_Feature = false)
        {
            HtmlAgilityPack.HtmlDocument doc = new();
            doc.LoadHtml(html);

            AdditionalOptions_Feature = addtionalOptions_Feature;

            this.doc = doc;

            if (!Init()) throw new Exception("Incorrect tags");
        }
        private bool Init()
        {
            string? text = ParseHtml(doc.DocumentNode);

            if (string.IsNullOrEmpty(text)) return false;

            _cleaner(ref text);

            try
            {
                if (!RegularExp.isFooter().IsMatch(text is not null ? text : throw new Exception("Invalid text.")))
                    throw new Exception("Incorrect tags.");
            }
            catch (Exception e) { RTConsole.Write(e.Message + "\n", Color.Red); return false; }

            RTConsole.Write("Start footer parsing...");

            try
            {
                _requirements = GetRequirements(ref text);

                RTConsole.Write("Requirements have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                if (AdditionalOptions_Feature)
                    _additionalOptions = GetAdditional_Options_Feature(ref text);
                else
                    _additionalOptions = GetAdditional_Options(ref text);

                RTConsole.Write("Additional Options have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                boostingMethods = GetBoosting_Methods(ref text);

                RTConsole.Write("Boosting Methods have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                var about = GetAbout(ref text);

                _aboutTitle = about.Item1;
                _aboutText = about.Item2;

                RTConsole.Write("About have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                faqs = GetFAQs(ref text);

                RTConsole.Write("FAQs have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            RTConsole.Write("Footer parse is complete.\n");

            return true;

            void _cleaner(ref string? input)
            {
                if (string.IsNullOrEmpty(input)) return;

                input = HttpUtility.HtmlDecode(input);
                input = RegularExp.GetUnnecessaryElements().Replace(input, string.Empty);
                input = RegularExp.GetUnnecessarySpaces().Replace(input, " ");
                input = input.Replace("\"", "\\\"");
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
                        case "strong":
                            if (child.ParentNode is not null)
                            {
                                result.Append($"<strong>");
                                result.Append(ParseHtml(child));
                                result.Append($"</strong>");
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
            if (!RegularExp.GetRequirements().IsMatch(input))
                throw new Exception("Requirements was not found. Check the name or structure if this a mistake. Block is ignored.");

            return RegularExp.GetRequirements().Match(input).Groups[1].Value;
        }
        private string GetAdditional_Options(ref string input)
        {
            if (!RegularExp.GetAdditional_Options().IsMatch(input))
                throw new Exception("Additional Options was not found. Check the name or structure if this a mistake. Block is ignored.");

            return RegularExp.GetAdditional_Options().Match(input).Groups[1].Value;
        }
        private string GetAdditional_Options_Feature(ref string input)
        {
            if (!RegularExp.GetAdditional_Options().IsMatch(input))
                throw new Exception("Additional Options was not found. Check the name or structure if this a mistake. Block is ignored.");

            string text = RegularExp.GetAdditional_Options().Match(input).Groups[1].Value;

            MatchCollection lists = RegularExp.GetAdditional_Options_Feature_List().Matches(text);

            foreach(Match match in lists)            
                text = Regex.Replace(text, match.Value, parseList(match.Value));            

            return text;

            string parseList(string input)
            {
                StringBuilder item = new();

                MatchCollection items = RegularExp.GetAdditional_Options_Feature_List_Items().Matches(input);

                item.Append($"<ul>");
                foreach(Match match in items)
                    item.Append($"<li><strong>{Regex.Replace(match.Groups[1].Value, @"<*?(?:\/?)strong>", string.Empty)}</strong>: {match.Groups[2].Value}</li>");
                item.Append($"</ul>");

                return item.ToString();
            }
        }
        private Dictionary<string, string> GetBoosting_Methods(ref string input)
        {
            if (!RegularExp.GetBoosting_Methods().IsMatch(input))
                throw new Exception("Boosting Methods was not found. Check the name or structure if this a mistake. Block is ignored.");

            string text = RegularExp.GetBoosting_Methods().Match(input).Groups[1].Value;

            text = Regex.Replace(text, @"<(\/?)strong>", string.Empty);

            Dictionary<string, string> boostingMethods = new();

            foreach (Match i in RegularExp.GetBoosting_Methods_Items().Matches(text))
                boostingMethods.Add(i.Groups[1].Value.ToLower() != "piloted" ? "Self-Play" : i.Groups[1].Value, Regex.Replace(i.Groups[2].Value, @"\s*$", string.Empty));

            return boostingMethods;
        }
        private Tuple<string, string> GetAbout(ref string input)
        {
            if (!RegularExp.GetAbout().IsMatch(input))
                throw new Exception("About was not found. Check the name or structure if this a mistake. Block is ignored.");

            var about = RegularExp.GetAbout().Matches(input);

            return new Tuple<string, string>(about[0].Groups[1].Value.Trim(), about[0].Groups[2].Value.Trim());
        }
        private Dictionary<string, string> GetFAQs(ref string input)
        {
            if (!RegularExp.GetFAQs().IsMatch(input))
                throw new Exception("FAQs was not found. Check the name or structure if this a mistake. Block is ignored.");            

            string text = RegularExp.GetFAQs().Match(input).Groups[1].Value;

            Dictionary<string, string> faqs = new();

            foreach (Match i in RegularExp.GetFAQs_Items().Matches(text))
                faqs.Add(Regex.Replace(i.Groups[1].Value, @"<(\/?)strong>", string.Empty), Regex.Replace(i.Groups[2].Value, @"\s*$", string.Empty));

            return faqs;
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
