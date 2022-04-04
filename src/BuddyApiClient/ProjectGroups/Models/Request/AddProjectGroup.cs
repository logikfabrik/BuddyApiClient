namespace BuddyApiClient.ProjectGroups.Models.Request
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Groups.Models;
    using EnsureThat;

    public sealed record AddProjectGroup
    {
        public AddProjectGroup(PermissionSet permissionSet)
        {
            PermissionSet = Ensure.Any.HasValue(permissionSet, nameof(permissionSet));
        }

        [JsonPropertyName("id")]
        public GroupId GroupId { get; set; }

        [JsonPropertyName("permission_set")]
        public PermissionSet PermissionSet { get; }
    }
}