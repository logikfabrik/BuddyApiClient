namespace BuddyApiClient.CurrentUserEmails.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models;

    public sealed record EmailList : ApiObject
    {
        [JsonPropertyName("emails")]
        public IEnumerable<EmailOutline>? Emails { get; set; }
    }
}