namespace BuddyApiClient.Projects.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record ProjectSummary : Document
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("name")]
        public ProjectName Name { get; set; }

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