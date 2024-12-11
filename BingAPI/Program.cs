using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    //private const string subscriptionKey = "b941aa4ab9104ce4bc0a38bd95a7176f";
    //private const string endpoint = "https://api.bing.microsoft.com/v7.0/spellcheck";
    //private const string mode = "proof"; // Use "proof" or "spell"

    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the text to spell and grammar check:");
        string textToCheck = Console.ReadLine();

        string text = "Ths is a smple text with some speling errors. We wants to chek how well the spell cheker works.";

        string subscriptionKey = "<SubscriptionKey>";
        string endpoint = "https://api.bing.microsoft.com/v7.0/spellcheck";
        string mode = "proof"; // Use "proof" or "spell"

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("mkt", "en-us"),
                new KeyValuePair<string, string>("mode", mode), // Ensures advanced grammar and style checking
                new KeyValuePair<string, string>("text", text)
            };

            var content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync(endpoint, content);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            var jsonObject = JObject.Parse(jsonResponse);
            var flaggedTokens = jsonObject["flaggedTokens"];
            return flaggedTokens.HasValues;



            //string result = await SpellCheckTextAsync(textToCheck);
            Console.WriteLine("Spell and Grammar Check Result:");
            Console.WriteLine(hasSpellingIssues);
            Console.WriteLine(flaggedTokens);
            Console.ReadKey();
        }
    }

    //static async Task<string> SpellCheckTextAsync(string text)
    //{
    //    using (HttpClient client = new HttpClient())
    //    {
    //        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

    //        var values = new System.Collections.Generic.List<KeyValuePair<string, string>>();
    //        values.Add(new KeyValuePair<string, string>("mkt", "en-us"));
    //        values.Add(new KeyValuePair<string, string>("mode", mode)); // Ensures advanced grammar and style checking
    //        values.Add(new KeyValuePair<string, string>("text", text));

    //        var content = new FormUrlEncodedContent(values);

    //        HttpResponseMessage response = await client.PostAsync(endpoint, content);
    //        string jsonResponse = await response.Content.ReadAsStringAsync();

    //        return FormatSpellCheckResult(jsonResponse);
    //    }
    //}

    //static string FormatSpellCheckResult(string jsonResponse)
    //{
    //    var jsonObject = JObject.Parse(jsonResponse);
    //    var flaggedTokens = jsonObject["flaggedTokens"];
    //    string result = "";

    //    foreach (var token in flaggedTokens)
    //    {
    //        string tokenValue = token["token"].ToString();
    //        string suggestion = token["suggestions"][0]["suggestion"].ToString();
    //        string issueType = token["type"].ToString(); // Type of issue (e.g., "UnknownToken", "RepeatedToken", etc.)
    //        result += $"Original: {tokenValue}, Suggestion: {suggestion}, Issue: {issueType}\n";
    //    }

    //    return result;
    //}
}
