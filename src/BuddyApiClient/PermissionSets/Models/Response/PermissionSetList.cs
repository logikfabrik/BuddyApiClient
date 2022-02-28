namespace BuddyApiClient.PermissionSets.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record PermissionSetList : Response
    {
        [JsonPropertyName("permission_sets")]
        public IEnumerable<PermissionSetSummary>? PermissionSets { get; set; }
    }
}