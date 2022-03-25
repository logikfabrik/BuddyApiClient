namespace BuddyApiClient.Members.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record MemberList : PageResponse
    {
        [JsonPropertyName("members")]
        public IEnumerable<MemberSummary> Members { get; set; } = Enumerable.Empty<MemberSummary>();

        public override int Count => Members.Count();
    }
}