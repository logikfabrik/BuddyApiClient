namespace BuddyApiClient.ProjectMembers.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record UpdateProjectMember
    {
        public UpdateProjectMember(PermissionSet permissionSet)
        {
            PermissionSet = Ensure.Any.HasValue(permissionSet, nameof(permissionSet));
        }

        [JsonPropertyName("permission_set")]
        public PermissionSet PermissionSet { get; }
    }
}