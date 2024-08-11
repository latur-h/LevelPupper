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
        private readonly string _fisrtBlock;
        private readonly string _secondBlock;

        public Header(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new();
            doc.LoadHtml(html);

            this.doc = doc;

            var split = Regex.Split(_parseHtml(doc.DocumentNode), @"<td>.*?<\/td>").Where(x => x.Length > 0).ToArray();

            _fisrtBlock = split[0];
            _secondBlock = split[1];

            Init();
        }

        private void Init()        
        {
            try
            {
                _preview = GetPreview(_fisrtBlock);
                _utp = GetUTP(_fisrtBlock);

                GetSEO(doc);

                _title = GetTitle(_secondBlock);
                _description = GetDescription(_secondBlock);
                _rewards = GetRewards(doc);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);

                Dispose();
            }
        }
        private void GetSEO(HtmlAgilityPack.HtmlDocument doc)
        {
            var rows = doc.DocumentNode.SelectNodes("//div//table//tr");

            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var row in rows)
            {
                var columns = row.SelectNodes("td");
                if (columns != null && columns.Count == 2)
                {
                    string key = columns[0].InnerText.Trim();
                    string value = columns[1].InnerText.Trim();
                    dict[key] = value;
                }
            }

            _seoDescription = HttpUtility.HtmlDecode(Regex.Replace(dict.ElementAt(0).Value, @"\&nbsp\;", string.Empty));
            _seoTitle = HttpUtility.HtmlDecode(Regex.Replace(dict.ElementAt(1).Value, @"\&nbsp\;", string.Empty));
            _seoURL = HttpUtility.HtmlDecode(Regex.Replace(dict.ElementAt(2).Value.Split('/').Where(x => x.Length > 0).Last(), @"\&nbsp\;", string.Empty));

            Console.WriteLine(_seoDescription);
        }
        private string _parseHtml(HtmlNode node)
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
                        case "td":
                        case "p":
                            result.Append($"<{child.Name}>");
                            result.Append(_parseHtml(child));
                            result.Append($"</{child.Name}>");
                            break;
                        default:
                            result.Append(_parseHtml(child));
                            break;
                    }
                }
            }
            return result.ToString();
        }
        private string GetPreview(string input) => HttpUtility.HtmlDecode(Regex.Replace(Regex.Matches(input, @"<h1>(.*?)<\/h1>")[0].Groups[1].Value, @"\&nbsp\;", string.Empty));
        private string GetUTP(string input)
        {
            var matches = Regex.Matches(input, @"<p>(.*?)<\/p>");

            StringBuilder utp = new();

            utp.Append(@"<ul>");
            foreach (var match in matches)            
                utp.Append($"<li>{(match as Match)?.Groups[1].Value}</li>");
            utp.Append(@"</ul>");

            return HttpUtility.HtmlDecode(Regex.Replace(utp.ToString(), @"\&nbsp\;", string.Empty));
        }
        private string GetTitle(string input) => HttpUtility.HtmlDecode(Regex.Replace(Regex.Matches(input, @"<h1>(.*?)<\/h1>")[0].Groups[1].Value, @"\&nbsp\;", string.Empty));
        private string GetDescription(string input) => HttpUtility.HtmlDecode(Regex.Replace(Regex.Matches(input, @"<\/h1>(.*?)<h2>")[0].Groups[1].Value, @"\&nbsp\;", string.Empty));        
        private string GetRewards(HtmlAgilityPack.HtmlDocument doc)
        {
            var split = Regex.Split(Regex.Replace(_parseHtml(doc.DocumentNode), @"\&nbsp\;", string.Empty), @"<h2>.*?</h2>").Where(x => x.Length > 0).ToArray();

            StringBuilder rewards = new();

            foreach (var i in Regex.Matches(split[1], @"<p>(.*?)<\/p>"))
                rewards.Append($"<div><h3>{(i as Match)?.Groups[1].Value}</h3></div>");

            return HttpUtility.HtmlDecode(rewards.ToString());
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