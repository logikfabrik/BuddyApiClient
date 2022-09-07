namespace BuddyApiClient.Pipelines.Models
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Projects.Models;
    using EnsureThat;

    public sealed record PipelineTriggerCondition : TriggerCondition
    {
        public PipelineTriggerCondition(string pipelineName, ProjectName projectName) : base(TriggerConditionType.SuccessPipeline)
        {
            PipelineName = Ensure.String.IsNotNullOrEmpty(pipelineName, nameof(pipelineName));
            ProjectName = projectName;
        }

        [JsonPropertyName("trigger_project_name")]
        public ProjectName ProjectName { get; }

        [JsonPropertyName("trigger_pipeline_name")]
        public string PipelineName { get; }
    }
}