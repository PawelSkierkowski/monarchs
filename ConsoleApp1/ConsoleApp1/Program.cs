// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Text.RegularExpressions;
using ConsoleApp1;
using Newtonsoft.Json;

var monarchs = JsonConvert.DeserializeObject<IList<Monarch>>(GetData());

if (monarchs != null)
{
    Console.WriteLine($"Monarchs count: {monarchs.Count}");
    var longestRuled = monarchs.Aggregate((i1, i2) => i1.YearsPeriod > i2.YearsPeriod ? i1 : i2);
    Console.WriteLine($"Longest ruled period: {longestRuled.YearsPeriod}");
    Console.WriteLine($"Longest ruled house: {longestRuled.House}");
    Console.WriteLine($"Longest ruled name: {longestRuled.Name}");

    var mostCommonFirstName = monarchs
        .GroupBy(i => Regex.Replace(i.Name.Split()[0], @"[^0-9a-zA-Z\ ]+", ""))
        .OrderByDescending(g => g.Count()).ThenBy(g => g.Key).First().Key;
    Console.WriteLine($"Most common first name: {mostCommonFirstName}");
    Console.Read();
}

string GetData()
{
    const string url =
        @"https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings";

    var request = (HttpWebRequest) WebRequest.Create(url);
    request.AutomaticDecompression = DecompressionMethods.GZip;

    using var response = (HttpWebResponse) request.GetResponse();
    using var stream = response.GetResponseStream();
    using var reader = new StreamReader(stream);
    return reader.ReadToEnd();
}