using Newtonsoft.Json;

namespace ConsoleApp1;

public class Monarch
{
    private const char YearSeparator = '-';

    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("nm")] public string Name { get; set; }
    [JsonProperty("cty")] public string City { get; set; }
    [JsonProperty("hse")] public string House { get; set; }
    [JsonProperty("yrs")] public string Years { get; set; }

    [JsonIgnore]
    public int YearsPeriod
    {
        get
        {
            if (!Years.Contains(YearSeparator)) return 1;
            var years = Years.Split(YearSeparator);
            if (years.All(x => x != string.Empty))
                return int.Parse(years[1]) - int.Parse(years[0]);
            return 1;
        }
    }
}