namespace BuddyApiClient.Pipelines.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record CreateOnEventPipeline : CreatePipeline
    {
        public CreateOnEventPipeline(string name, IEnumerable<Event> events) : base(name, TriggerMode.Event)
        {
            Events = Ensure.Any.HasValue(events, nameof(events));
        }

        [JsonPropertyName("events")]
        public IEnumerable<Event> Events { get; }
    }
}