namespace BuddyApiClient.Pipelines.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public abstract record CreatePipeline
    {
        protected CreatePipeline(string name, TriggerMode mode)
        {
            Name = Ensure.String.IsNotNullOrEmpty(name, nameof(name));
            Mode = mode;
        }

        protected CreatePipeline(string name, TriggerMode mode, string worker) : this(name, mode)
        {
            WorkerAssignmentMethod = Models.WorkerAssignmentMethod.Fixed;
            Worker = Ensure.String.IsNotNullOrEmpty(worker, nameof(worker));
        }

        protected CreatePipeline(string name, TriggerMode mode, IEnumerable<string> tags) : this(name, mode)
        {
            WorkerAssignmentMethod = Models.WorkerAssignmentMethod.Tags;
            Tags = Ensure.Any.HasValue(tags);
        }

        [JsonPropertyName("name")]
        public string Name { get; }


        [JsonIgnore]
        public TriggerMode Mode { get; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Mode" />.
        /// </summary>
        [JsonPropertyName("on")]
        public string ModeJson => TriggerModeJsonConverter.ConvertTo(Mode);

        [JsonPropertyName("trigger_conditions")]
        public IEnumerable<TriggerCondition>? Conditions { get; set; }

        [JsonPropertyName("always_from_scratch")]
        public bool? AlwaysFromScratch { get; set; }

        [JsonPropertyName("auto_clear_cache")]
        public bool? AutoClearCache { get; set; }

        [JsonPropertyName("no_skip_to_most_recent")]
        public bool? NoSkipToMostRecent { get; set; }

        [JsonPropertyName("do_not_create_commit_status")]
        public bool? DoNotCreateCommitStatus { get; set; }

        [JsonPropertyName("ignore_fail_on_project_status")]
        public bool? IgnoreFailOnProjectStatus { get; set; }

        [JsonPropertyName("execution_message_template")]
        public string? ExecutionMessageTemplate { get; set; } = "$BUDDY_EXECUTION_REVISION_SUBJECT";

        [JsonIgnore]
        public WorkerAssignmentMethod? WorkerAssignmentMethod { get; }

        [JsonPropertyName("worker_assignment")]
        public string? WorkerAssignmentMethodJson => WorkerAssignmentMethodJsonConverter.TryConvertTo(WorkerAssignmentMethod, out var json) ? json : null;

        [JsonPropertyName("worker")]
        public string? Worker { get; }

        [JsonPropertyName("tags")]
        public IEnumerable<string>? Tags { get; }

        [JsonPropertyName("target_site_url")]
        public Uri? TargetSiteUrl { get; set; }

        [JsonPropertyName("disabled")]
        public bool? Disabled { get; set; }

        [JsonPropertyName("disabled_reason")]
        public string? DisabledReason { get; set; }
    }
}