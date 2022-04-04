namespace BuddyApiClient.ProjectGroups.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record UpdateProjectGroup
    {
        public UpdateProjectGroup(PermissionSet permissionSet)
        {
            PermissionSet = Ensure.Any.HasValue(permissionSet, nameof(permissionSet));
        }

        [JsonPropertyName("permission_set")]
        public PermissionSet PermissionSet { get; }
    }
}