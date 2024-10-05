using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LevelPupper__Parser.dlls.API
{
    class API_Pupser : IDisposable
    {
        private string? login { get; }
        private string? password { get; }
        private string? url_Login { get; }

        private string? url_Service { get; }
        private string? url_GameService { get; }

        private string? url_ElementOfDescription { get; }
        private string? url_RangeGradation { get; }
        private string? url_ValueOption { get; }

        private Product? _product { get; set; }

        public API_Pupser(string game, string codename, API_Pupser_Configuration pupser)
        {
            login = pupser.login;
            password = pupser.password;
            url_Login = pupser.url_Login;

            url_Service = pupser.url_Service;
            url_GameService = pupser.url_GameService;

            url_ElementOfDescription = pupser.url_ElementOfDescription;
            url_RangeGradation = pupser.url_RangeGradation;
            url_ValueOption = pupser.url_ValueOption;

            Init(game, codename);
        }

        private void Init(string game, string codename)
        {
            using (HttpClient client = new HttpClient())
            {
                var payload = new
                {
                    game_codename = game,
                    service_codename = codename,
                    anon_session_id = "",
                    service_token = ""
                };

                string jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = client.PostAsync(url_Service, content).GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                        _product = JsonConvert.DeserializeObject<Product>(responseContent);

                        if (_product is null) throw new Exception("Invalid data.");
                    }
                    else
                    {
                        RTConsole.Write("Request failed with status code: " + response.StatusCode, Color.Red);
                    }
                }
                catch (Exception ex)
                {
                    RTConsole.Write("Error: " + ex.Message, Color.Red);
                }
            }
        }

        public string GetServices()
        {
            StringBuilder rtfText = new StringBuilder($@"{{\rtf1\ansi {{\field{{\*\fldinst HYPERLINK ""{url_GameService}{_product?.service?.id}""}}{{\fldrslt {_product?.service?.title ?? "Title"}}}}}\par");

            rtfText.AppendLine($@"\par Description elements:\par");

            IEnumerable<DescriptionElement>? descriptionElements = _product?.service?.description_elements?.Where(x => x.elements?.Count > 0);

            if (descriptionElements is not null)
                foreach (var i in descriptionElements)
                {
                    rtfText.AppendLine($@"\tab {i.title}:\par");

                    if (i.elements is not null)
                        foreach (var element in i.elements)
                            rtfText.AppendLine($@"\tab\tab {{\field{{\*\fldinst HYPERLINK ""{url_ElementOfDescription}{element.id}""}}{{\fldrslt {element.title}}}}}\par");
                }

            string base_price = string.Empty;
            if (_product?.service?.price_from is not null && _product.service?.price_from > 0) base_price = _product.service?.price_from.ToString() + "$";
            rtfText.AppendLine($@"\par Base price - {base_price} \par");

            string preview_price = string.Empty;
            if (_product?.service?.price_from_display is not null && _product.service?.price_from_display > 0) preview_price = _product.service?.price_from_display.ToString() + "$";
            rtfText.AppendLine($@"Preview price - {preview_price}\par");

            rtfText.AppendLine($@"Price prefix - {_product?.service?.price_prefix}\par");
            rtfText.AppendLine($@"Price sufix - {_product?.service?.price_sufix}\par");

            string preview_discount_price = string.Empty;
            if (_product?.service?.preview_discount_price is not null && _product.service?.preview_discount_price > 0) preview_discount_price = _product.service?.preview_discount_price.ToString() + "$";
            rtfText.AppendLine($@"Preview discount price - {preview_discount_price}\par");

            rtfText.AppendLine(@"\par Value options:\par");

            if (_product?.options is not null)
                foreach (var i in _product.options)
                {
                    if (i.range_gradations?.Count > 0)
                    {
                        rtfText.AppendLine($@"\tab {i.title}:\par");

                        foreach (var element in i.range_gradations)
                        {
                            string price = "Free";

                            if (element.price is not null) price = element.price.ToString() + "$";                           

                            rtfText.AppendLine($@"\tab\tab {{\field{{\*\fldinst HYPERLINK ""{url_RangeGradation}{element.id}""}}{{\fldrslt {element.title}}}}} - {price}\par");
                        }
                    }

                    if (i.values_options?.Count > 0)
                    {
                        rtfText.AppendLine($@"\tab {i.title}:\par");

                        foreach (var element in i.values_options)
                        {
                            string price = "Free";

                            if (element.price_amount is not null) price = element.price_amount.ToString() + "$";
                            else if (element.price_percent is not null) price = element.price_percent.ToString() + "%";

                            rtfText.AppendLine($@"\tab\tab {{\field{{\*\fldinst HYPERLINK ""{url_ValueOption}{element.id}""}}{{\fldrslt {element.title}}}}} - {price}\par");
                        }
                    }
                }

            rtfText.AppendLine(@"\par \par}");

            return rtfText.ToString();
        }

        public async void Save(string text)
        {
            var matches = RegularExp.GetOption().Matches(text);

            if (matches is null) throw new Exception("Invalid text.");

            var valueOptions = matches.Where(x => x.Success && x.Groups["ServiceType"].Success && x.Groups["ServiceType"].Value == ServiceType["Value option"]);
            var rangeGradations = matches.Where(x => x.Success && x.Groups["ServiceType"].Success && x.Groups["ServiceType"].Value == ServiceType["Range gradation"]);

            var values = _product?.options?
                .Where(x => x.values_options is not null && x.values_options.Count > 0)
                .Select(x => x.values_options).SelectMany(x => x)
                .ToDictionary(x => x.id.ToString() ?? string.Empty, x => x);

            var ranges = _product?.options?
                .Where(x => x.range_gradations is not null && x.range_gradations.Count > 0)
                .Select(x => x.range_gradations).SelectMany(x => x)
                .ToDictionary(x => x.id.ToString() ?? string.Empty, x => x);

            var valuesOptionsPayloads = new Dictionary<string, Dictionary<string, string>>();
            var rangeGradationPayloads = new Dictionary<string, Dictionary<string, string>>();

            if (values is not null)
                foreach (var i in values)
                {
                    Match newValueOption = valueOptions.First(x => x.Groups["Id"].ToString() == i.Key);

                    if (i.Value.price_type.ToString() == GetTypeID(newValueOption.Groups["priceType"].Value))
                    {
                        if (i.Value.price_type == 1) continue;
                        else if (i.Value.price_type == 2 && i.Value.price_amount == decimal.Parse(newValueOption.Groups["price"].Value)) continue;
                        else if (i.Value.price_type == 3 && i.Value.price_percent == decimal.Parse(newValueOption.Groups["price"].Value)) continue;
                    }

                    Dictionary<string, string> payload = new();

                    payload.Add("csrfmiddlewaretoken", "");
                    payload.Add("game", _product?.service?.game?.id.ToString() ?? string.Empty);
                    payload.Add("title", i.Value.title ?? string.Empty);

                    payload.Add("price_type", GetTypeID(newValueOption.Groups["priceType"].Value));

                    payload.Add("price_amount", "");
                    payload.Add("preview_price_amount", i.Value.preview_price_amount.ToString() ?? string.Empty);
                    payload.Add("price_percent", "");

                    switch (GetTypeID(newValueOption.Groups["priceType"].Value))
                    {
                        case "2":
                            payload["price_amount"] = newValueOption.Groups["price"].Value;
                            break;
                        case "3":
                            payload["price_percent"] = newValueOption.Groups["price"].Value;
                            break;
                    }

                    if (i.Value.checked_by_default == true)
                        payload.Add("checked_by_default", "on");

                    payload.Add("_continue", "Save and continue editing");

                    valuesOptionsPayloads.Add(i.Value.id.ToString() ?? string.Empty, payload);
                }

            if (ranges is not null)
                foreach (var i in ranges)
                {
                    Match newValueOption = rangeGradations.First(x => x.Groups["Id"].ToString() == i.Key);

                    if (i.Value.price == decimal.Parse(newValueOption.Groups["price"].Value)) continue;

                    Dictionary<string, string> payload = new();

                    payload.Add("csrfmiddlewaretoken", "");
                    payload.Add("title", i.Value.title ?? string.Empty);
                    payload.Add("title_display", i.Value.title_display ?? string.Empty);
                    payload.Add("title_color", i.Value.title_color ?? string.Empty);
                    payload.Add("number_from", i.Value.number_from.ToString() ?? string.Empty);
                    payload.Add("number_to", i.Value.number_to.ToString() ?? string.Empty);
                    payload.Add("step", i.Value.step.ToString() ?? string.Empty);
                    payload.Add("price", newValueOption.Groups["price"].Value);
                    payload.Add("additional_etc_minutes", i.Value.additional_etc_minutes.ToString() ?? string.Empty);
                    payload.Add("_continue", "Save and continue editing");

                    rangeGradationPayloads.Add(i.Value.id.ToString() ?? string.Empty, payload);
                }


            #region Login
            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };

            using (HttpClient client = new HttpClient(handler))
            {
                client.Timeout = TimeSpan.FromMinutes(5);
                client.DefaultRequestHeaders.ConnectionClose = false;
                client.DefaultRequestHeaders.ExpectContinue = false;

                HttpResponseMessage loginPageResponse = await client.GetAsync(url_Login);
                string loginPageContent = await loginPageResponse.Content.ReadAsStringAsync();

                string csrfToken = "";
                var tokenMatch = Regex.Match(loginPageContent, @"<input type=""hidden"" name=""csrfmiddlewaretoken"" value=""([^""]+)""");
                if (tokenMatch.Success)
                    csrfToken = tokenMatch.Groups[1].Value;
                else
                    throw new Exception("CSRF token not found!");

                var loginData = new Dictionary<string, string>
                {
                    { "csrfmiddlewaretoken", csrfToken },
                    { "username", login },
                    { "password", password },
                    { "next", "/admin/" }
                };

                HttpContent loginContent = new FormUrlEncodedContent(loginData);
                client.DefaultRequestHeaders.Referrer = new Uri(url_Login);

                HttpResponseMessage loginResponse = await client.PostAsync(url_Login, loginContent);

                if (!loginResponse.IsSuccessStatusCode) throw new Exception($"Login failed with status code: {loginResponse.StatusCode}");

                #region API Calls
                if (valuesOptionsPayloads.Count > 0)
                    foreach (var i in valuesOptionsPayloads)
                    {
                        HttpResponseMessage targetPageResponse = await client.GetAsync(url_ValueOption + i.Key + @"/change/");
                        string targetPageContent = await targetPageResponse.Content.ReadAsStringAsync();

                        string targetCsrfToken = "";
                        var targetTokenMatch = Regex.Match(targetPageContent, @"<input type=""hidden"" name=""csrfmiddlewaretoken"" value=""([^""]+)""");
                        if (targetTokenMatch.Success)
                        {
                            targetCsrfToken = targetTokenMatch.Groups[1].Value;
                        }
                        else throw new Exception("CSRF token not found on target page!");

                        i.Value["csrfmiddlewaretoken"] = targetCsrfToken;

                        HttpContent postContent = new FormUrlEncodedContent(i.Value);

                        HttpResponseMessage postResponse = await client.PostAsync(url_ValueOption + i.Key + @"/change/", postContent);

                        if (postResponse.IsSuccessStatusCode)
                        {
                            await postResponse.Content.ReadAsStringAsync();
                            RTConsole.Write($"{i.Key} - is succecfully changed.", Color.Green);
                        }
                        else
                            throw new Exception($"POST request failed with status code: {postResponse.StatusCode}");
                    }

                if (rangeGradationPayloads.Count > 0)
                    foreach (var i in rangeGradationPayloads)
                    {
                        HttpResponseMessage targetPageResponse = await client.GetAsync(url_RangeGradation + i.Key + @"/change/");
                        string targetPageContent = await targetPageResponse.Content.ReadAsStringAsync();

                        string targetCsrfToken = "";
                        var targetTokenMatch = Regex.Match(targetPageContent, @"<input type=""hidden"" name=""csrfmiddlewaretoken"" value=""([^""]+)""");
                        if (targetTokenMatch.Success)
                        {
                            targetCsrfToken = targetTokenMatch.Groups[1].Value;
                        }
                        else throw new Exception("CSRF token not found on target page!");

                        i.Value["csrfmiddlewaretoken"] = targetCsrfToken;

                        HttpContent postContent = new FormUrlEncodedContent(i.Value);

                        HttpResponseMessage postResponse = await client.PostAsync(url_RangeGradation + i.Key + @"/change/", postContent);

                        if (postResponse.IsSuccessStatusCode)
                        {
                            await postResponse.Content.ReadAsStringAsync();
                            RTConsole.Write($"{i.Key} - is succecfully changed.", Color.Green);
                        }
                        else
                            throw new Exception($"POST request failed with status code: {postResponse.StatusCode}");
                    }
                #endregion
            }
            #endregion

            string GetTypeID(string type)
            {
                string id = string.Empty;

                switch (type)
                {
                    case "$":
                        id = "2";
                        break;
                    case "%":
                        id = "3";
                        break;
                    case "Free":
                        id = "1";
                        break;
                }

                return id;
            }
            async void POST(HttpClient client, string url, string id, Dictionary<string, string> payload)
            {
                HttpResponseMessage targetPageResponse = await client.GetAsync(url + id);
                string targetPageContent = await targetPageResponse.Content.ReadAsStringAsync();

                string targetCsrfToken = "";
                var targetTokenMatch = Regex.Match(targetPageContent, @"<input type=""hidden"" name=""csrfmiddlewaretoken"" value=""([^""]+)""");
                if (targetTokenMatch.Success)
                {
                    targetCsrfToken = targetTokenMatch.Groups[1].Value;
                }
                else throw new Exception("CSRF token not found on target page!");

                payload["csrfmiddlewaretoken"] = targetCsrfToken;

                HttpContent postContent = new FormUrlEncodedContent(payload);

                HttpResponseMessage postResponse = await client.PostAsync(url, postContent);

                if (postResponse.IsSuccessStatusCode)
                    RTConsole.Write($"{id} - is succecfully changed.", Color.Green);
                else
                    throw new Exception($"POST request failed with status code: {postResponse.StatusCode}");
            }
        }
        private Dictionary<string, string> ServiceType = new() 
        {
            { "Value option", "valueoption" },
            { "Range gradation", "rangegradation" }
        };
        public void Dispose()
        {

        }
    }
}
