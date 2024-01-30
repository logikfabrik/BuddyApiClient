namespace BuddyApiClient.Pipelines.Models.Response
{
    using BuddyApiClient.Core.Models.Response;
    using System.Text.Json.Serialization;

    public sealed record PipelineDetails : DocumentResponse
    {
        [JsonPropertyName("id")]
        public PipelineId Id { get; set; }
    }
}
