namespace BuddyApiClient.Members.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models;

    public sealed record MemberList : ApiObject
    {
        [JsonPropertyName("members")]
        public IEnumerable<MemberOutline>? Members { get; set; }
    }
}