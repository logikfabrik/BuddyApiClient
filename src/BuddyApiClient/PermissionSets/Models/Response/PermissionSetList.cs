namespace BuddyApiClient.PermissionSets.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record PermissionSetList : CollectionResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("permission_sets")]
        public IEnumerable<PermissionSetSummary> PermissionSets { get; set; } = Enumerable.Empty<PermissionSetSummary>();
    }
}