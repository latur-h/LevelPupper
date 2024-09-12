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

                        js = Regex.Replace(js, @"{&advantages&}", header._utp ?? string.Empty);
                        js = Regex.Replace(js, @"{&description&}", header._description ?? string.Empty);
                        js = Regex.Replace(js, @"{&reward&}", header._rewards ?? string.Empty);
                        js = Regex.Replace(js, @"{&seoDescription&}", header._seoDescription ?? string.Empty);
                        js = Regex.Replace(js, @"{&seoTitle&}", header._seoTitle ?? string.Empty);
                        js = Regex.Replace(js, @"{&title&}", header._title ?? string.Empty);
                        js = Regex.Replace(js, @"{&preview&}", header._preview ?? string.Empty);
                        js = Regex.Replace(js, @"{&url&}", header._seoURL ?? string.Empty);

                        return js;
                    }
                case Script.Description:
                    if (footer is null)
                        throw new Exception("Footer is null there");

                    using (StreamReader reader = new(pathToDescriptionJS, Encoding.Default))
                    {
                        string js = reader.ReadToEnd();

                        js = Regex.Replace(js, @"{&aboutTitle&}", footer._aboutTitle ?? "About");
                        js = Regex.Replace(js, @"{&boostingMethod&}", footer.boostingMethods is null ? "0" : footer.boostingMethods.Count.ToString());
                        js = Regex.Replace(js, @"{&requirements&}", footer._requirements ?? string.Empty);
                        js = Regex.Replace(js, @"{&additionalOptions&}", footer._additionalOptions ?? string.Empty);
                        js = Regex.Replace(js, @"{&aboutText&}", footer._aboutText ?? string.Empty);

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
