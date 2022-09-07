namespace BuddyApiClient.Pipelines.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record CreateOnSchedulePipeline : CreatePipeline
    {
        public CreateOnSchedulePipeline(string name, DateTime startDate) : base(name, TriggerMode.Schedule)
        {
            StartDate = Ensure.Any.HasValue(startDate, nameof(startDate));
        }

        public CreateOnSchedulePipeline(string name, int delayInMinutes) : base(name, TriggerMode.Schedule)
        {
            DelayInMinutes = delayInMinutes;
        }

        public CreateOnSchedulePipeline(string name, string cron) : base(name, TriggerMode.Schedule)
        {
            Cron = Ensure.String.IsNotNullOrEmpty(cron, nameof(cron));
        }

        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; }

        [JsonPropertyName("delay")]
        public int? DelayInMinutes { get; }

        [JsonPropertyName("cron")]
        public string? Cron { get; }

        [JsonPropertyName("paused")]
        public bool Paused { get; set; }

        [JsonPropertyName("refs")]
        public IEnumerable<string>? References { get; set; }
    }
}