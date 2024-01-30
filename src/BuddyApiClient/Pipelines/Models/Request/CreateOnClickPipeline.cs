namespace BuddyApiClient.Pipelines.Models.Request
{
    using System.Text.Json.Serialization;

    public sealed record CreateOnClickPipeline : CreatePipeline
    {
        public CreateOnClickPipeline(string name) : base(name, TriggerMode.Click)
        {
        }

        [JsonPropertyName("refs")]
        public IEnumerable<string>? References { get; set; }
    }
}