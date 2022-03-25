namespace BuddyApiClient.CurrentUserEmails.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record EmailList : Response
    {
        [JsonPropertyName("emails")]
        public IEnumerable<EmailSummary> Emails { get; set; } = Enumerable.Empty<EmailSummary>();
    }
}