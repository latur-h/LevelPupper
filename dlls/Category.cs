using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace LevelPupper__Parser.dlls
{
    class Category : IDisposable
    {
        public string? _seoDescription;
        public string? _seoTitle;
        public string? _seoURL;

        public string? _title;
        public string? _shortDescription;

        public string? _pageDescription;

        private readonly HtmlAgilityPack.HtmlDocument doc;

        public Category(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new();
            doc.LoadHtml(html);

            this.doc = doc;

            Init();
        }
        private void Init()
        {
            string? text = ParseHtml(doc.DocumentNode);

            _cleaner(ref text);

            RemoveSEO(ref text);

            if (string.IsNullOrEmpty(text)) throw new Exception("Invalid text!");

            RTConsole.Write("Start category parsing...");

            try
            {
                var seo = GetSEO(doc);

                _seoDescription = seo.Item1;
                _seoTitle = seo.Item2;
                _seoURL = seo.Item3;

                RTConsole.Write("Seo have been succecfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                var head = GetHead(ref text);

                _title = head.Item1;
                _shortDescription = head.Item2;

                RTConsole.Write("Head have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                _pageDescription = GetPageDescription(ref text);

                RTConsole.Write("Page description have been successfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            RTConsole.Write("Category parse is complete.\n");

            void _cleaner(ref string? input)
            {
                if (string.IsNullOrEmpty(input)) return;

                input = HttpUtility.HtmlDecode(input);
                input = RegularExp.GetUnnecessaryElements().Replace(input, string.Empty);
                input = RegularExp.GetUnnecessarySpaces().Replace(input, " ");
                input = input.Replace("\"", "\\\"");
            }
        }
        private Tuple<string, string> GetHead(ref string input)
        {
            if (!RegularExp.GetHead().IsMatch(input))
                throw new Exception("Head was not found. Check the name or structure if this a mistake. Block is ignored.");

            var head = RegularExp.GetHead().Match(input);

            return new Tuple<string, string>(Regex.Replace(head.Groups[1].Value, @"<(\/?)strong>", string.Empty), head.Groups[2].Value);
        }
        private string GetPageDescription(ref string input)
        {
            if (!RegularExp.GetPageDescription().IsMatch(input))
                throw new Exception("Page description was not found. Check the name or structure if this a mistake. Block is ignored.");

            var pageDescription = RegularExp.GetPageDescription().Match(input).Groups[1];

            return pageDescription.Value;
        }
        private Tuple<string, string, string> GetSEO(HtmlAgilityPack.HtmlDocument doc)
        {
            var rows = doc.DocumentNode.SelectNodes("//div//table//tr");

            if (rows is null || rows.Count == 0) throw new Exception("SEO was not found. Check the structure or formatting settings if this a mistake. Block is ignored.");

            List<string> seoTable = new();

            foreach (var row in rows)
            {
                HtmlNodeCollection columns = row.SelectNodes("td");

                if (columns is not null && columns.Count == 2)
                    seoTable.Add(_cleaner(columns[1].InnerText.Trim()));
                else
                {
                    columns = row.SelectNodes("th");

                    seoTable.Add(_cleaner(columns[1].InnerText.Trim()));
                }
            }

            return new Tuple<string, string, string>(seoTable[0], seoTable[1], seoTable[2].Split('/').Where(x => x.Length > 0).Last());

            string _cleaner(string? input)
            {
                input = HttpUtility.HtmlDecode(input);
                input = input?.Replace("\"", "\\\"");
                input = Regex.Replace(input is null ? string.Empty : input, @"\s*$", string.Empty);

                return input;
            }
        }
        private void RemoveSEO(ref string? input)
        {
            if (string.IsNullOrEmpty(input)) return;

            input = Regex.Replace(input, @"<tr>.*<\/tr>", string.Empty);
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
                        case "h1":
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

        public void Dispose()
        {

        }
    }
}
