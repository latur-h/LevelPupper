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
        private readonly string pathToCategoryJS;

        public enum Script
        {
            General,
            Description,
            Category
        }

        public JavaScriptBuilder(string general, string description, string category)
        {
            pathToGeneralJS = general;
            pathToDescriptionJS = description;
            pathToCategoryJS = category;
        }

        public string Build(Script script, Header? header = null, Footer? footer = null, Ccategory? category = null, bool? isAboutNullifier = null)
        {
            switch (script)
            {
                case Script.General:
                    if (header is null)
                        throw new Exception("Header is null there.");

                    using (StreamReader reader = new(pathToGeneralJS, Encoding.Default))
                    {
                        StringBuilder js = new(reader.ReadToEnd());

                        js.AppendLine($"try{{ insertStaticText(" +
                            $"\"{isNull(header._defaultPossition, "1")}\"," +
                            $"\"{isNull(header._seoURL)}\"," +
                            $"\"{isNull(header._preview)}\"," +
                            $"\"{isNull(header._title)}\"," +
                            $"\"{isNull(header._seoTitle)}\"," +
                            $"\"{isNull(header._seoDescription)}\"" +
                            $"); }} catch {{}}");

                        js.AppendLine($"try{{ await insertText(" +
                            $"\"{isNull(header._utp)}\"," +
                            $"\"{isNull(header._description)}\"," +
                            $"\"{isNull(header._rewards)}\"" +
                            $"); }} catch {{}}");

                        return js.ToString();
                    }
                case Script.Description:
                    if (footer is null)
                        throw new Exception("Footer is null there.");

                    using (StreamReader reader = new(pathToDescriptionJS, Encoding.Default))
                    {
                        StringBuilder js = new(reader.ReadToEnd());

                        js.AppendLine($"try{{ insertStaticText(\"{isNull(footer._aboutTitle)}\"); }} catch {{}}");

                        js.AppendLine($"try{{ await insertText(" +
                            $"\"{isNull(footer._requirements)}\"," +
                            $"\"{isNull(footer._additionalOptions)}\"," +
                            $"\"{isNull(footer._aboutText)}\"" +
                            $"); }} catch {{}}");

                        js.AppendLine("try{ changeSelectElement(\"id_description_elements-2-type\", \"0\"); } catch {}");
                        js.AppendLine($"try{{ changeSelectElement(\"id_description_elements-6-type\", \"{(footer.boostingMethods is null ? "" : footer.boostingMethods.Count.ToString())}\"); }} catch {{}}");

                        if (isAboutNullifier ?? false && footer._aboutTitle is null && footer._aboutText is null)
                        {
                            js.AppendLine($"try{{ document.getElementById(\"id_description_elements-13-show_title\").checked = false; }} catch {{}}");
                            js.AppendLine($"try{{ document.getElementById(\"id_description_elements-14-show_title\").checked = false; }} catch {{}}");

                            js.AppendLine($"try{{ changeSelectElement(\"id_description_elements-13-type\", \"0\"); }} catch {{}}");
                            js.AppendLine($"try{{ changeSelectElement(\"id_description_elements-14-type\", \"0\"); }} catch {{}}");
                        }

                        int count = 1;
                        foreach (var i in footer.boostingMethods is null ? new Dictionary<string, string>() : footer.boostingMethods)
                            js.AppendLine($"try{{ await executeFunction(\"#add_id_description_elements-6-elements\", {count++}, \"{i.Key}\", \"{i.Value}\"); }} catch {{}}");

                        count = 1;
                        foreach (var i in footer.faqs is null ? new Dictionary<string, string>() : footer.faqs)
                            js.AppendLine($"try{{ await executeFunction(\"#add_id_description_elements-16-elements\", {count++}, \"{i.Key}\", \"{i.Value}\"); }} catch {{}}");

                        return js.ToString();
                    }
                case Script.Category:
                    if (category is null) 
                        throw new Exception("Category is null there.");

                    using (StreamReader reader = new(pathToCategoryJS, Encoding.Default))
                    {
                        StringBuilder js = new(reader.ReadToEnd());

                        js.AppendLine($"try{{ insertStaticText(" +
                            $"\"{isNull(category._seoURL)}\"," +
                            $"\"{isNull(category._title)}\"," +
                            $"\"{isNull(category._seoTitle)}\"," +
                            $"\"{isNull(category._seoDescription)}\"" +
                            $"); }} catch {{}}");

                        js.AppendLine($"try{{ await insertText(" +
                            $"\"{isNull(category._shortDescription)}\"," +
                            $"\"{isNull(category._pageDescription)}\"" +
                            $"); }} catch {{}}");

                        return js.ToString();
                    }
                default:
                    return string.Empty;
            }

            string isNull(string? text, string? customValue = null)
            {
                return string.IsNullOrEmpty(text) ? customValue ?? string.Empty : text;
            }
        }
    }
}
