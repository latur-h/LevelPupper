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
    class Header : IDisposable
    {
        public string? _preview;
        public string? _utp;

        public string? _seoDescription;
        public string? _seoTitle;
        public string? _seoURL;

        public string? _title;
        public string? _description;

        public string? _rewards;

        private readonly HtmlAgilityPack.HtmlDocument doc;

        public Header(string html)
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

            RTConsole.Write("Start header parsing...");

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
                _rewards = GetRewards(ref text);

                RemoveRewards(ref text);

                RTConsole.Write("Rewards have been succecfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                var title = GetTitle(ref text);

                _title = title.Item1;
                _preview = title.Item2;

                if (!string.IsNullOrEmpty(_title))
                    RTConsole.Write("Title have been succecfully parsed.", Color.Green);

                if (!string.IsNullOrEmpty(_preview))
                    RTConsole.Write("Preview have been succecfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            try
            {
                var descriptions = GetDescription(ref text);

                _description = descriptions.Item1;
                _utp = descriptions.Item2;

                if(!string.IsNullOrEmpty(_utp))
                    RTConsole.Write("UTP have been succecfully parsed.", Color.Green);

                if (!string.IsNullOrEmpty(_description))
                    RTConsole.Write("Description have been succecfully parsed.", Color.Green);
            }
            catch (Exception e) { RTConsole.Write(e.Message, Color.Red); }

            RTConsole.Write("Header parse is complete.\n");

            static void _cleaner(ref string? input)
            {
                if (string.IsNullOrEmpty(input))
                    return;

                input = HttpUtility.HtmlDecode(input);

                if (RegularExp.isFooter().IsMatch(input))
                {
                    input = RegularExp.GetUnnecessaryFooter().Replace(input, string.Empty);

                    RTConsole.Write(@"Footer detected! Removed due the rules for headers.", Color.Red);
                }
                input = RegularExp.GetUnnecessaryElements().Replace(input, string.Empty);
                input = RegularExp.GetUnnecessarySpaces().Replace(input, " ");
                input = input.Replace("\"", "\\\"");
            }
        }
        
        private Tuple<string?, string?> GetTitle(ref string? input)
        {
            if (string.IsNullOrEmpty(input)) 
                throw new Exception("Title was not found. Check the structure or formatting settings if this a mistake. Block is ignored.");

            MatchCollection titles = RegularExp.GetTitle().Matches(input);
            
            if (titles.Count > 2 || titles.Count < 1)
                throw new Exception("Title was not found. Check the structure or formatting settings if this a mistake. Block is ignored.");

            if (titles.Count == 2)
            {
                string preview = titles[0].Groups[1].Value.Length <= titles[1].Groups[1].Value.Length ? titles[0].Groups[1].Value : titles[1].Groups[1].Value;
                string title = titles[0].Groups[1].Value.Length >= titles[1].Groups[1].Value.Length ? titles[0].Groups[1].Value : titles[1].Groups[1].Value;

                return new Tuple<string?, string?>(title, preview);
            }
            else
            {
                RTConsole.Write("Preview was not found. Check the structure or formatting settings if this a mistake. Block is ignored.", Color.Red);

                return new Tuple<string?, string?>(titles[0].Groups[1].Value, null);
            }
        }
        private Tuple<string?, string?> GetDescription(ref string? input)
        {
            if (string.IsNullOrEmpty(input))
                throw new Exception("Descriptions was not found. Check the structure or formatting settings if this a mistake. Block is ignored.");

            MatchCollection descriptions = RegularExp.GetDescription().Matches(input);

            if (descriptions.Count < 1)
                throw new Exception("Descriptions was not found. Check the structure or formatting settings if this a mistake. Block is ignored.");

            StringBuilder utp = new();

            var utps = descriptions.Where(x => x.Groups[1].Value.Split(' ').Length <= 5).ToArray();

            if (utps is not null && utps.Length != 0)
            {
                utp.Append(@"<ul>");
                foreach (var i in utps)
                    utp.Append($"<li>{i.Groups[1].Value}</li>");
                utp.Append(@"</ul>");
            }
            else
                RTConsole.Write("UTP was not found. Check the structure or formatting settings if this a mistake. Block is ignored.", Color.Red);

            var _descriptions = descriptions.Where(x => x.Groups[1].Value.Split(' ').Length > 5).ToArray();

            StringBuilder description = new();

            if (_descriptions is not null && _descriptions.Length != 0)
            {
                foreach (var i in _descriptions)
                    description.Append($"<p>{i.Groups[1].Value}</p>");
            }
            else
                RTConsole.Write("Description was not found. Check the structure or formatting settings if this a mistake. Block is ignored.", Color.Red);

            return new Tuple<string?, string?>(description.ToString(), utp.ToString());
        }
        private string? GetRewards(ref string? input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            MatchCollection rewards = RegularExp.GetRewards().Matches(input);

            if (rewards.Count < 1)
                throw new Exception("Rewards was not found. Check the structure or formatting settings if this a mistake. Block is ignored.");

            StringBuilder _rewards = new();

            foreach (Match match in rewards)
                foreach (Capture capture in match.Groups[1].Captures)
                    _rewards.Append($"<div><h3>{capture.Value}</h3></div>");

            return _rewards.ToString();
        }
        private void RemoveRewards(ref string? input)
        {
            if (string.IsNullOrEmpty(input)) return;

            input = RegularExp.GetRewards().Replace(input, string.Empty);
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

            input = Regex.Replace(input, @"<td>.*<\/td>", string.Empty); 
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

        public void Dispose()
        {
            _preview = string.Empty;
            _utp = string.Empty;

            _seoDescription = string.Empty;
            _seoTitle = string.Empty;
            _seoURL = string.Empty;

            _rewards = string.Empty;
            _title = string.Empty;
            _description = string.Empty;
        }
    }
}