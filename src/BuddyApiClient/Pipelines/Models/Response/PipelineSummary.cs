namespace BuddyApiClient.Pipelines.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record PipelineSummary : Document
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("id")]
        public PipelineId Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("on")]
        public string? On { get; set; }

        [JsonPropertyName("refs")]
        public IEnumerable<string>? Refs { get; set; }

        [JsonIgnore]
        public ExecutionStatus LastExecutionStatus { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="LastExecutionStatus" />.
        /// </summary>
        [JsonPropertyName("last_execution_status")]
        public string LastExecutionStatusJson
        {
            set => LastExecutionStatus = ExecutionStatusJsonConverter.ConvertFrom(value);
        }

        [JsonPropertyName("last_execution_revision")]
        public string? LastExecutionRevision { get; set; }
    }
}
