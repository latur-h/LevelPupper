using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LevelPupper__Parser.dlls
{
    public partial class RegularExp
    {
        [GeneratedRegex(@"(?>Description.*Title.*URL)|(?><h2>(?>\s*)Reward[s]?(?>\s*)</h2>)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex isHeader();
        [GeneratedRegex(@"(<h2>(?>\s*)Requirement[s](?>\s*)<\/h2>)|(<h2>(?>\s*)Additional\sOptions(?>\s*)<\/h2>)|(<h[23]>(?>\s*)Boosting\sMethod[s]?(?>\s*)<\/h[23]>)|(<h2>(?>\s*)(About\s.*?)(?>\s*)<\/h2>)|(<h[23]>(?>\s*)FAQ[s]?(?>\s*)<\/h[23]>)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex isFooter();

        [GeneratedRegex(@"(H[23]\s*-\s*)|(УТП[1-5])|(\[link\!\]|(?=<))", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetUnnecessaryElements();
        [GeneratedRegex(@"(\x20|\xA0)+", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetUnnecessarySpaces();
        [GeneratedRegex(@"(<h2>(?>\s*)Requirement[s](?>\s*)<\/h2>(.*))|(<h2>(?>\s*)Additional\sOptions(?>\s*)<\/h2>(.*))|(<h[23]>(?>\s*)Boosting\sMethod[s]?(?>\s*)<\/h[23]>(.*))|(<h2>(?>\s*)(About\s.*?)(?>\s*)<\/h2>(.*))|(<h2>(?>\s*)FAQ[s]?(?>\s*)<\/h2>(.*))", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetUnnecessaryFooter();

        #region Header
        [GeneratedRegex(@"<h1>(?>\s*)([^<]+)(?>\s*)<\/h1>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetTitle();
        [GeneratedRegex(@"<p>(?>\s*)([^<]+)(?>\s*)<\/p>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetDescription();
        [GeneratedRegex(@"<h2>(?>\s*)Reward[s]?(?>\s*)</h2>(?>\s*)(?:\s*<p>([^<]+)</p>)+", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetRewards();
        #endregion

        #region Footer
        [GeneratedRegex(@"<h2>(?>\s*)Requirements(?>\s*)<\/h2>(?>\s*)(.*?)(?>\s*)(?=<h[23]>|(?>\s*)$)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetRequirements();
        [GeneratedRegex(@"<h2>(?>\s*)Additional\sOptions(?>\s*)<\/h2>(?>\s*)(.*?)(?>\s*)(?=<h[23]>|(?>\s*)$)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetAdditional_Options();
        [GeneratedRegex(@"<ul>(?>\s*)(?:.*?)(?>\s*)</ul>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetAdditional_Options_Feature_List();
        [GeneratedRegex(@"<li>(?>\W)*(.*?)(?:\s*)(?::|\u2013)(?:\s)*(.*?)(?>\s)*<\/li>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetAdditional_Options_Feature_List_Items();
        [GeneratedRegex(@"<h[23]>(?>\s*)Boosting\sMethod[s]?(?>\s*)<\/h[23]>(?>\s*)(.*?)(?>\s*)(?=<h[23]>|(?>\s*)$)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetBoosting_Methods();
        [GeneratedRegex(@"<li>(?>\W)*(piloted|self[\W]*?play)(?>[\W])*(.*?)<\/li>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetBoosting_Methods_Items();
        [GeneratedRegex(@"<h2>(?>\s*)(About\s.*?)(?>\s*)<\/h2>(.*?)(?=<h[23]>(?>\s*)FAQ[s]?(?>\s*)<\/h[23]>|(?>\s*)$)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetAbout();
        [GeneratedRegex(@"<h[23]>(?>\s*)FAQ[s]?(?>\s*)<\/h[23]>(?>\s*)(.*?)(?>\s*)$", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetFAQs();
        [GeneratedRegex(@"<h3>(?>[\W])*?(.*?)<\/h3>(?=\s)*?(?>[\W]*?)(.*?)((?=<h3>)|$)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetFAQs_Items();
        #endregion

        #region Category
        [GeneratedRegex(@"<h1>(?>\s*)(.*?)(?>\s*)</h1>(?>\s*)(.*?)(?>\s*)<h[23]>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetHead();
        [GeneratedRegex(@"(?>\s*)(<h[23]>.*?)(?>\s*)$", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetPageDescription();
        #endregion

        #region API
        [GeneratedRegex(@"""(?:https://api\.levelupper\.com/admin/game_services/)(?'ServiceType'.*?)/(?'Id'\d+)""(?:.*?)-\s(?'price'[-]?\d+([.]\d+)?)?(?'priceType'[$%]|Free)", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex GetOption();
        #endregion
    }
}
