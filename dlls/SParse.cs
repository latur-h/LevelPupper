using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LevelPupper__Parser.dlls.API;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace LevelPupper__Parser.dlls
{
    internal class SParse : IDisposable
    {
        private readonly string? login;
        private readonly string? password;

        private readonly bool? isSilent;
        private readonly bool? isForce;

        ChromeOptions options;

        public SParse(API_Pupser_Configuration config, bool? isSilent = null, bool? isForce = null)
        {
            login = config.login;
            password = config.password;

            this.isSilent = isSilent;
            this.isForce = isForce;

            options = GetOptions(this.isSilent);
        }
        public void AddNewItem(Header header, Footer footer, string game)
        {
            using (IWebDriver driver = new ChromeDriver(options))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

                Login(driver, wait);

                driver.Navigate().GoToUrl("https://api.levelupper.com/admin/game_services/gameservice/add/"); // Static
                wait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").ToString() == "complete");

                string id = ApplyHeader(driver, wait, header, game);

                ApplyDefaultDescriptionElements(driver, wait, game, id);

                driver.Navigate().GoToUrl($"https://api.levelupper.com/admin/game_services/gameservice/{id}/change/?_changelist_filters=game__id__exact%3D{Games.GetOptionValue(game)}"); // Variable
                wait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").ToString() == "complete");

                ApplyFooter(driver, wait, footer, id, game);

                driver.Quit();
            }
        }
        private void Login(IWebDriver driver, WebDriverWait wait)
        {
            driver.Navigate().GoToUrl("https://api.levelupper.com/admin/login/?next=/admin/");

            IWebElement? element = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.Name("username"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });

            IWebElement login = driver.FindElement(By.Name("username"));
            login.SendKeys(this.login);
            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys(this.password);
            password.SendKeys(OpenQA.Selenium.Keys.Enter);

            wait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").ToString() == "complete");
        }
        private string ApplyHeader(IWebDriver driver, WebDriverWait wait, Header header, string gameName)
        {
            IWebElement? isHidden = driver.FindElement(By.Id("id_hidden"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", isHidden);

            IWebElement? game = driver.FindElement(By.Name("game"));
            SelectElement select = new SelectElement(game);
            select.SelectByText(gameName); // Variable

            IWebElement? boosterSpecs = driver.FindElement(By.Name("booster_specs"));
            SelectElement boosterSpec = new SelectElement(boosterSpecs);
            foreach (var i in boosterSpec.Options.Where(x => x.Text.ToLower().Contains(Games.GetBoostSpec(gameName)))) // Variable
                boosterSpec.SelectByText(i.Text);

            IWebElement? possition = driver.FindElement(By.Name("pos"));
            ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{header._defaultPossition ?? "1"}"";", possition); // Variable

            IWebElement? codename = driver.FindElement(By.Name("codename"));
            ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{Games.GetCodename(gameName)}"";", codename); // Variable

            driver.Navigate().GoToUrl("https://api.levelupper.com/admin/game_services/gameservice/add/#/tab/module_1/");

            IWebElement? preview = driver.FindElement(By.Name("short_title"));
            if (header._preview != null)
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{header._preview}"";", preview); // Variable

            IWebElement? UTPSource = driver.FindElement(By.Id("cke_27"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", UTPSource);
            IWebElement? UTP = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.CssSelector("[title='Rich Text Editor, id_short_description']"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });
            if (header._utp != null)
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{header._utp}"";", UTP); // Variable
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", UTPSource);

            IWebElement? title = driver.FindElement(By.Name("title"));
            if (header._title != null)
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{header._title}"";", title); // Variable

            IWebElement? descriptionSource = driver.FindElement(By.Id("cke_104"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", descriptionSource);
            IWebElement? description = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.CssSelector("[title='Rich Text Editor, id_description']"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });
            if (header._description != null)
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{header._description}"";", description); // Variable
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", descriptionSource);

            IWebElement? rewardsSource = driver.FindElement(By.Id("cke_180"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", rewardsSource);
            IWebElement? rewards = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.CssSelector("[title='Rich Text Editor, id_rewards_description']"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });
            if (header._rewards != null)
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{header._rewards}"";", rewards); // Variable
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", rewardsSource);

            IWebElement saveandContinue = driver.FindElement(By.Name("_continue"));
            saveandContinue.Click();

            wait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").ToString() == "complete");

            return Regex.Match(driver.Url, @"/(?'id'\d+)/").Groups["id"].Value;
        }
        private void ApplyDefaultDescriptionElements(IWebDriver driver, WebDriverWait wait, string gameName, string id)
        {
            driver.Navigate().GoToUrl($"https://api.levelupper.com/admin/game_services/gameservice/?game__id__exact={Games.GetOptionValue(gameName)}"); // Variable

            IWebElement? gamesTable = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.Id("result_list"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });

            IList<IWebElement>? rows = gamesTable?.FindElements(By.TagName("tr"));

            if (!rows.Any())
                throw new Exception();

            foreach (var row in rows)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));

                if (cells.Count == 7)
                {
                    IWebElement cell = cells[3];

                    string href = cell.FindElement(By.TagName("a")).GetAttribute("href");

                    if (href.Contains(id)) // Variable
                    {
                        IWebElement checkbox = cells[0];
                        checkbox.Click();

                        break;
                    }
                }
            }

            IWebElement? actions = driver.FindElement(By.Name("action"));
            SelectElement action = new SelectElement(actions);
            action.SelectByIndex(2);

            IWebElement go = driver.FindElements(By.Name("index")).Where(x => x.Text.ToLower() == "go").First();
            go.Click();
        }
        private void ApplyFooter(IWebDriver driver, WebDriverWait wait, Footer footer, string id, string gameName)
        {
            driver.Navigate().GoToUrl($"https://api.levelupper.com/admin/game_services/gameservice/{id}/change/?_changelist_filters=game__id__exact%3D{Games.GetOptionValue(gameName)}#/tab/inline_2/");
            wait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").ToString() == "complete");

            IWebElement? requirements1 = driver.FindElement(By.Name("description_elements-0-title"));
            ((IJavaScriptExecutor)driver).ExecuteScript(@"arguments[0].value = ""Requirements"";", requirements1);

            IWebElement? requirementsShowTitle = driver.FindElement(By.Name("description_elements-1-show_title"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", requirementsShowTitle);
            IWebElement? requirements2 = driver.FindElement(By.Name("description_elements-1-title"));
            ((IJavaScriptExecutor)driver).ExecuteScript(@"arguments[0].value = ""Requirements"";", requirements2);

            IWebElement? requirementsTextSource = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.Id("cke_400"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", requirementsTextSource);
            IWebElement? requirements = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.CssSelector("[title='Rich Text Editor, id_description_elements-1-content']"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });
            if (footer._requirements != null)
            ((IJavaScriptExecutor)driver).ExecuteScript(@$"arguments[0].value = ""{footer._requirements}"";", requirements); // Variable
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", requirementsTextSource);

            IWebElement? youMayAlsoLike = driver.FindElement(By.Name("description_elements-2-type"));
            SelectElement youMayAlsoLikeSelect = new SelectElement(youMayAlsoLike);
            youMayAlsoLikeSelect.SelectByIndex(0);
            IWebElement? youMayAlsoLikeShowTitle1 = driver.FindElement(By.Name("description_elements-2-show_title"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = false;", youMayAlsoLikeShowTitle1);

            IWebElement? youMayAlsoLikeTitle = driver.FindElement(By.Name("description_elements-3-title"));
            ((IJavaScriptExecutor)driver).ExecuteScript(@"arguments[0].value = ""You May Also Like"";", youMayAlsoLikeTitle);
            IWebElement? youMayAlsoLikeShowTitle2 = driver.FindElement(By.Name("description_elements-3-show_title"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", youMayAlsoLikeShowTitle2);

            IWebElement? additinalOptionShowTitle = driver.FindElement(By.Name("description_elements-5-show_title"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", additinalOptionShowTitle);
            IWebElement? additinalOptionsTitle = driver.FindElement(By.Name("description_elements-5-title"));
            ((IJavaScriptExecutor)driver).ExecuteScript(@"arguments[0].value = ""Additional Options"";", additinalOptionsTitle);
            IWebElement? additinalOptionsSource = driver.FindElement(By.Id("cke_704"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", additinalOptionsSource);
            IWebElement? additinalOptions = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.CssSelector("[title='Rich Text Editor, id_description_elements-5-content']"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });
            if (footer._additionalOptions != null)
            ((IJavaScriptExecutor)driver).ExecuteScript(@$"arguments[0].value = ""{footer._additionalOptions}"";", additinalOptions); // Variable
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", additinalOptionsSource);

            IWebElement? boostingMethods = driver.FindElement(By.Name("description_elements-6-type"));
            SelectElement boostingMethod = new SelectElement(boostingMethods);
            switch (footer?.boostingMethods?.Count)
            {
                case 1:
                    boostingMethod.SelectByValue("blocks_one"); // Variable
                    break;
                case 2:
                    boostingMethod.SelectByValue("blocks_two"); // Variable
                    break;
            }
            IWebElement? boostingMethodsShowTitle = driver.FindElement(By.Name("description_elements-6-show_title"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = false;", boostingMethodsShowTitle);
            IWebElement? boostingMethodsShowCard = driver.FindElement(By.Name("description_elements-6-with_card"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", boostingMethodsShowCard);

            IWebElement? addBoostingMethods = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.Id("add_id_description_elements-6-elements"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });

            int counterBM = 1;

            Thread.Sleep(1000);
            foreach (var i in footer.boostingMethods)
            {
                Thread.Sleep(500);
                addBoostingMethods?.Click();

                IWebElement? relatedPopupDivBoostingMethod = wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(By.CssSelector(".related-popup"));
                        if (elem.Displayed)
                            return elem;
                    }
                    catch (NoSuchElementException) { }
                    return null;
                });
                IWebElement? iframeBoostingMethod = relatedPopupDivBoostingMethod?.FindElement(By.TagName("iframe"));

                driver.SwitchTo().Frame(iframeBoostingMethod);

                IWebElement? boostingMethodsPossition = driver.FindElement(By.Name("pos"));
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{counterBM++}"";", boostingMethodsPossition); // Variable

                IWebElement? boostingMethodsTitle = driver.FindElement(By.Name("title"));
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{i.Key}"";", boostingMethodsTitle); // Variable

                IWebElement? boostingMethodsSource = wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(By.Id("cke_19"));
                        if (elem.Displayed)
                            return elem;
                    }
                    catch (NoSuchElementException) { }
                    return null;
                });

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", boostingMethodsSource);
                IWebElement? boostingMethodsElemenets = wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(By.CssSelector("[title='Rich Text Editor, id_content']"));
                        if (elem.Displayed)
                            return elem;
                    }
                    catch (NoSuchElementException) { }
                    return null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript(@$"arguments[0].value = ""{i.Value}"";", boostingMethodsElemenets); // Variable
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", boostingMethodsSource);

                IWebElement boostingMethodsSave = driver.FindElement(By.Name("_save"));
                boostingMethodsSave.Click();

                driver.SwitchTo().DefaultContent();
            }

            IWebElement? whychooseUsShowTitle = driver.FindElement(By.Name("description_elements-8-show_title"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", whychooseUsShowTitle);
            IWebElement? whyChooseUs = driver.FindElement(By.Id("id_description_elements-8-title"));
            ((IJavaScriptExecutor)driver).ExecuteScript(@"arguments[0].value = ""Why Choose Us"";", whyChooseUs);

            IWebElement? howitworksShowTitle = driver.FindElement(By.Name("description_elements-10-show_title"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", howitworksShowTitle);
            IWebElement? howitworks = driver.FindElement(By.Id("id_description_elements-10-title"));
            ((IJavaScriptExecutor)driver).ExecuteScript(@"arguments[0].value = ""How It Works"";", howitworks);

            IWebElement? completionTimeShowTitle = driver.FindElement(By.Name("description_elements-12-show_title"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", completionTimeShowTitle);
            IWebElement? completionTime = driver.FindElement(By.Id("id_description_elements-12-title"));
            ((IJavaScriptExecutor)driver).ExecuteScript(@"arguments[0].value = ""Completion Time"";", completionTime);

            if (footer._aboutTitle is null)
            {
                IWebElement? about1 = driver.FindElement(By.Name("description_elements-13-type"));
                SelectElement about1_Select = new SelectElement(about1);
                about1_Select.SelectByIndex(0);

                IWebElement? aboutShowTitle1 = driver.FindElement(By.Name("description_elements-13-show_title"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", aboutShowTitle1);

                IWebElement? about2 = driver.FindElement(By.Name("description_elements-14-type"));
                SelectElement about2_Select = new SelectElement(about2);
                about2_Select.SelectByIndex(0);

                IWebElement? aboutShowTitle2 = driver.FindElement(By.Name("description_elements-14-show_title"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", aboutShowTitle2);
            }
            else
            {
                IWebElement? aboutShowTitle = driver.FindElement(By.Name("description_elements-14-show_title"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", aboutShowTitle);

                IWebElement? aboutTitle = driver.FindElement(By.Name("description_elements-14-title"));
                if(footer._aboutTitle != null)
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{footer._aboutTitle}"";", aboutTitle); // Variable

                IWebElement? aboutTextSource = wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(By.Id("cke_1388"));
                        if (elem.Displayed)
                            return elem;
                    }
                    catch (NoSuchElementException) { }
                    return null;
                });

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", aboutTextSource);
                IWebElement? aboutText = wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(By.CssSelector("[title='Rich Text Editor, id_description_elements-14-content']"));
                        if (elem.Displayed)
                            return elem;
                    }
                    catch (NoSuchElementException) { }
                    return null;
                });
                if (footer._aboutText != null)
                ((IJavaScriptExecutor)driver).ExecuteScript(@$"arguments[0].value = ""{footer._aboutText}"";", aboutText); // Variable
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", aboutTextSource);
            }

            IWebElement? faqsShowTitle = driver.FindElement(By.Name("description_elements-16-show_title"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", faqsShowTitle);

            IWebElement? faqsTitle = driver.FindElement(By.Name("description_elements-16-title"));
            ((IJavaScriptExecutor)driver).ExecuteScript(@"arguments[0].value = ""FAQs"";", faqsTitle);

            IWebElement? addFAQs = wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(By.Id("add_id_description_elements-16-elements"));
                    if (elem.Displayed)
                        return elem;
                }
                catch (NoSuchElementException) { }
                return null;
            });

            int counterFAQ = 1;

            Thread.Sleep(1000);
            foreach(var i in footer.faqs)
            {
                Thread.Sleep(500);

                addFAQs?.Click();

                IWebElement? relatedPopupDivFaq = wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(By.CssSelector(".related-popup"));
                        if (elem.Displayed)
                            return elem;
                    }
                    catch (NoSuchElementException) { }
                    return null;
                });
                IWebElement? iframeFAQ = relatedPopupDivFaq?.FindElement(By.TagName("iframe"));

                driver.SwitchTo().Frame(iframeFAQ);

                IWebElement? faqPossition = driver.FindElement(By.Name("pos"));
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{counterFAQ++}"";", faqPossition); // Variable

                IWebElement? faqTitle = driver.FindElement(By.Name("title"));
                ((IJavaScriptExecutor)driver).ExecuteScript($@"arguments[0].value = ""{i.Key}"";", faqTitle); // Variable

                IWebElement? faqSource = wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(By.Id("cke_19"));
                        if (elem.Displayed)
                            return elem;
                    }
                    catch (NoSuchElementException) { }
                    return null;
                });

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", faqSource);
                IWebElement? faqElemenets = wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(By.CssSelector("[title='Rich Text Editor, id_content']"));
                        if (elem.Displayed)
                            return elem;
                    }
                    catch (NoSuchElementException) { }
                    return null;
                });
                ((IJavaScriptExecutor)driver).ExecuteScript(@$"arguments[0].value = ""{i.Value}"";", faqElemenets); // Variable
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", faqSource);

                IWebElement faqsSave = driver.FindElement(By.Name("_save"));
                faqsSave.Click();

                driver.SwitchTo().DefaultContent();
            }

            Thread.Sleep(1000);
            IWebElement saveorder = driver.FindElement(By.Name("_save"));
            saveorder.Click();
        }

        public Dictionary<string, string> GetTextFromSurferSEO(Dictionary<string, string> surferUrls)
        {
            Parallel.ForEach(surferUrls, (i) =>
            {
                IWebDriver surfer = new ChromeDriver(options);

                try
                {
                    surfer.Navigate().GoToUrl(i.Key);

                    WebDriverWait wait = new WebDriverWait(surfer, TimeSpan.FromSeconds(100));

                    IWebElement? text = wait.Until(driver =>
                    {
                        try
                        {
                            var div = driver.FindElement(By.CssSelector("div.tiptap.ProseMirror"));
                            if (div.Displayed)
                                return div;
                        }
                        catch (NoSuchElementException) { }
                        return null;
                    });

                    string? html = text?.GetAttribute("outerHTML");

                    if (string.IsNullOrEmpty(html))
                        throw new Exception();

                    surferUrls[i.Key] = html;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                finally { surfer.Quit(); }
            });

            return surferUrls;
        }

        private ChromeOptions GetOptions(bool? silent)
        {
            ChromeOptions options = new();

            options.PageLoadStrategy = PageLoadStrategy.Default;

            if(silent is not null && silent == true)
            {
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");                
            }

            options.AddArgument("--window-size=1920,1080");

            return options;
        }

        public void Dispose()
        {

        }
    }
}
