namespace BuddyApiClient.Pipelines.Models
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record PathsTriggerCondition : TriggerCondition
    {
        public PathsTriggerCondition(IEnumerable<string> paths) : base(TriggerConditionType.OnChangeAtPath)
        {
            Paths = Ensure.Any.HasValue(paths, nameof(paths));
        }

        [JsonPropertyName("trigger_condition_paths")]
        public IEnumerable<string> Paths { get; }
    }
}