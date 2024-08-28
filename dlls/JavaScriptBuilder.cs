using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LevelPupper__Parser.dlls
{
    internal class JavaScriptBuilder
    {
        private readonly string pathToGeneralJS;
        private readonly string pathToDescriptionJS;

        public enum Script
        {
            General,
            Description
        }

        public JavaScriptBuilder(string general, string description)
        {
            pathToGeneralJS = general;
            pathToDescriptionJS = description;
        }

        public string Build(Script script, Header? header = null, Footer? footer = null)
        {
            switch (script)
            {
                case Script.General:
                    if (header is null)
                        throw new Exception("Header is null there.");

                    using (StreamReader reader = new(pathToGeneralJS, Encoding.Default))
                    {
                        string js = reader.ReadToEnd();

                        js = Regex.Replace(js, @"{&advantages&}", header._utp is null ? string.Empty : header._utp);
                        js = Regex.Replace(js, @"{&description&}", header._description is null ? string.Empty : header._description);
                        js = Regex.Replace(js, @"{&reward&}", header._rewards is null ? string.Empty : header._rewards);
                        js = Regex.Replace(js, @"{&seoDescription&}", header._seoDescription is null ? string.Empty : header._seoDescription);
                        js = Regex.Replace(js, @"{&seoTitle&}", header._seoTitle is null ? string.Empty : header._seoTitle);
                        js = Regex.Replace(js, @"{&title&}", header._title is null ? string.Empty : header._title);
                        js = Regex.Replace(js, @"{&preview&}", header._preview is null ? string.Empty : header._preview);
                        js = Regex.Replace(js, @"{&url&}", header._seoURL is null ? string.Empty : header._seoURL);

                        return js;
                    }
                case Script.Description:
                    if (footer is null)
                        throw new Exception("Footer is null there");

                    using (StreamReader reader = new(pathToDescriptionJS, Encoding.Default))
                    {
                        string js = reader.ReadToEnd();

                        js = Regex.Replace(js, @"{&aboutTitle&}", footer._aboutTitle is null ? string.Empty : footer._aboutTitle);
                        js = Regex.Replace(js, @"{&boostingMethod&}", footer.boostingMethods is null ? "0" : footer.boostingMethods.Count.ToString());
                        js = Regex.Replace(js, @"{&requirements&}", footer._requirements is null ? string.Empty : footer._requirements);
                        js = Regex.Replace(js, @"{&additionalOptions&}", footer._additionalOptions is null ? string.Empty : footer._additionalOptions);
                        js = Regex.Replace(js, @"{&aboutText&}", footer._aboutText is null ? string.Empty : footer._aboutText);

                        if (footer._aboutTitle is null && footer._aboutText is null)
                        {
                            js += $"document.getElementById(\"id_description_elements-13-show_title\").checked = false;\n";
                            js += $"document.getElementById(\"id_description_elements-14-show_title\").checked = false;\n";

                            js += $"changeSelectElement(\"id_description_elements-13-type\", \"0\");\n";
                            js += $"changeSelectElement(\"id_description_elements-14-type\", \"0\");\n";
                        }

                        int count = 1;
                        foreach (var i in footer.boostingMethods is null ? new Dictionary<string, string>() : footer.boostingMethods)
                            js += $"await executeFunction(\"#add_id_description_elements-6-elements\", {count++}, \"{i.Key}\", \"{i.Value}\");\n";

                        count = 1;
                        foreach (var i in footer.faqs is null ? new Dictionary<string, string>() : footer.faqs)
                            js += $"await executeFunction(\"#add_id_description_elements-16-elements\", {count++}, \"{i.Key}\", \"{i.Value}\");\n";

                        return js;
                    }
                default:
                    return string.Empty;
            }
        }
    }
}
