namespace BuddyApiClient.Variables.Models.Request
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Pipelines.Models;

    public sealed record Pipeline : IScope
    {
        [JsonPropertyName("id")]
        public PipelineId Id { get; set; }
    }
}