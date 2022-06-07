namespace BuddyApiClient.Members.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record MemberList : CollectionPageResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("members")]
        public IEnumerable<MemberSummary> Members { get; set; } = Enumerable.Empty<MemberSummary>();

        public override int Count => Members.Count();
    }
}