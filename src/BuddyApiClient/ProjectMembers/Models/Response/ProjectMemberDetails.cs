namespace BuddyApiClient.ProjectMembers.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.PermissionSets.Models.Response;

    public sealed record ProjectMemberDetails : MemberDetails
    {
        [JsonPropertyName("permission_set")]
        public PermissionSetSummary? PermissionSet { get; set; }
    }
}