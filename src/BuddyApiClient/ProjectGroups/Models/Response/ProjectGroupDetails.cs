namespace BuddyApiClient.ProjectGroups.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Groups.Models.Response;
    using BuddyApiClient.PermissionSets.Models.Response;

    public sealed record ProjectGroupDetails : GroupDetails
    {
        [JsonPropertyName("permission_set")]
        public PermissionSetSummary? PermissionSet { get; set; }
    }
}