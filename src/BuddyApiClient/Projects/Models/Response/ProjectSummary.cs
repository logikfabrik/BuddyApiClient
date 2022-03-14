namespace BuddyApiClient.Projects.Models.Response
{
    using System.Text.Json.Serialization;

    public sealed record ProjectSummary
    {
        [JsonPropertyName("url")]
        public Uri? Url { get; set; }

        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonIgnore]
        public Status Status { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Status" />.
        /// </summary>
        [JsonPropertyName("status")]
        public string StatusJson
        {
            set => Status = StatusJsonConverter.ConvertFrom(value);
        }
    }
}