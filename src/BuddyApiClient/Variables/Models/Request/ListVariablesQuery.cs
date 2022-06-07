namespace BuddyApiClient.Variables.Models.Request
{
    using BuddyApiClient.Actions.Models;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Projects.Models;

    public sealed record ListVariablesQuery : Query
    {
        public ProjectName? ProjectName { get; set; }

        public PipelineId? PipelineId { get; set; }

        public ActionId? ActionId { get; set; }

        private string? GetProjectName()
        {
            return ProjectName?.Value;
        }

        private string? GetPipelineId()
        {
            return PipelineId?.Value.ToString();
        }

        private string? GetActionId()
        {
            return ActionId?.Value.ToString();
        }

        private void AddProjectName(QueryStringParameters parameters)
        {
            parameters.Add("projectName", GetProjectName);
        }

        private void AddPipelineId(QueryStringParameters parameters)
        {
            parameters.Add("pipelineId", GetPipelineId);
        }

        private void AddActionId(QueryStringParameters parameters)
        {
            parameters.Add("actionId", GetActionId);
        }

        protected override void AddParameters(QueryStringParameters parameters)
        {
            AddProjectName(parameters);
            AddPipelineId(parameters);
            AddActionId(parameters);
        }
    }
}