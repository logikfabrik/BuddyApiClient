namespace BuddyApiClient.Variables.Models.Request
{
    using BuddyApiClient.Core.Models.Request;

    public sealed record ListVariablesQuery : PageQuery
    {
        public string? ProjectName { get; set; }

        public int? PipelineId { get; set; }

        public int? ActionId { get; set; }
    }
}