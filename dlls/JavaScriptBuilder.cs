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
                        throw new Exception();

                    using (StreamReader reader = new(pathToGeneralJS, Encoding.Default))
                    {
                        string js = reader.ReadToEnd();

                        js = Regex.Replace(js, @"{&advantages&}", header._utp);
                        js = Regex.Replace(js, @"{&description&}", header._description);
                        js = Regex.Replace(js, @"{&reward&}", header._rewards);
                        js = Regex.Replace(js, @"{&seoDescription&}", header._seoDescription);
                        js = Regex.Replace(js, @"{&seoTitle&}", header._seoTitle);
                        js = Regex.Replace(js, @"{&title&}", header._title);
                        js = Regex.Replace(js, @"{&preview&}", header._preview);
                        js = Regex.Replace(js, @"{&url&}", header._seoURL);

                        return js;
                    }
                case Script.Description:
                    if (footer is null)
                        throw new Exception();

                    using (StreamReader reader = new(pathToDescriptionJS, Encoding.Default))
                    {
                        string js = reader.ReadToEnd();

                        js = Regex.Replace(js, @"{&aboutTitle&}", footer._aboutTitle);
                        js = Regex.Replace(js, @"{&boostingMethod&}", footer.boostingMethods.Count.ToString());
                        js = Regex.Replace(js, @"{&requirements&}", footer._requirements);
                        js = Regex.Replace(js, @"{&additionalOptions&}", footer._additionalOptions);
                        js = Regex.Replace(js, @"{&aboutText&}", footer._aboutText);

                        int count = 1;
                        foreach (var i in footer.boostingMethods)
                            js += $"await executeFunction(\"#add_id_description_elements-6-elements\", {count++}, \"{i.Key}\", \"{i.Value}\");\n";

                        count = 1;
                        foreach (var i in footer.faqs)
                            js += $"await executeFunction(\"#add_id_description_elements-16-elements\", {count++}, \"{i.Key}\", \"{i.Value}\");\n";

                        return js;
                    }
                default:
                    return string.Empty;
            }
        }
    }
}
