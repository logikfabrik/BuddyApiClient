namespace BuddyApiClient.Members.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record AddProjectMember
    {
        public AddProjectMember(PermissionSet permissionSet)
        {
            PermissionSet = Ensure.Any.HasValue(permissionSet, nameof(permissionSet));
        }

        [JsonPropertyName("id")]
        public MemberId MemberId { get; set; }

        [JsonPropertyName("permission_set")]
        public PermissionSet PermissionSet { get; }
    }
}