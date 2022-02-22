namespace BuddyApiClient.PermissionSets.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models;

    public sealed record PermissionSetList : ApiObject
    {
        [JsonPropertyName("permission_sets")]
        public IEnumerable<PermissionSetOutline>? PermissionSets { get; set; }
    }
}