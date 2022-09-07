namespace BuddyApiClient.Actions.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Pipelines.Models.Response;

    public record ActionDetails : DocumentResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("id")]
        public ActionId Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("trigger_time")]
        public string? TriggerTime { get; set; }

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

        [JsonPropertyName("deployment_excludes")]
        public IEnumerable<string>? DeploymentExcludes { get; set; }

        [JsonPropertyName("pipeline")]
        public PipelineSummary? Pipeline { get; set; }
    }
}