namespace BuddyApiClient.Members.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.PermissionSets.Models.Response;

    public sealed record ProjectMemberDetails : MemberDetails
    {
        [JsonPropertyName("permission_set")]
        public PermissionSetSummary? PermissionSet { get; set; }
    }
}