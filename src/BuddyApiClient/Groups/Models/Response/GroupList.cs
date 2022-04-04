namespace BuddyApiClient.Groups.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record GroupList : Response
    {
        [JsonPropertyName("groups")]
        public IEnumerable<GroupSummary> Groups { get; set; } = Enumerable.Empty<GroupSummary>();
    }
}