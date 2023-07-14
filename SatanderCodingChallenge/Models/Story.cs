using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class Story
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("url")]
    public string Uri { get; set; }

    [JsonProperty("by")]
    public string PostedBy { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTimeOffset Time { get; set; }

    [JsonProperty("score")]
    public int Score { get; set; }

    [JsonProperty("descendants")]
    public int CommentCount { get; set; }

    [JsonProperty("formattedTime")]
    public string FormattedTime => Time.ToString("yyyy-MM-dd HH:mm:ss");
}
