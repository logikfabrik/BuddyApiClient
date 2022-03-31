namespace BuddyApiClient.GroupMembers.Models.Request
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Members.Models;

    public sealed record AddGroupMember
    {
        [JsonPropertyName("id")]
        public MemberId MemberId { get; set; }
    }
}