namespace BuddyApiClient.ProjectMembers.Models.Request
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.PermissionSets.Models;

    public sealed record PermissionSet
    {
        [JsonPropertyName("id")]
        public PermissionSetId Id { get; set; }
    }
}