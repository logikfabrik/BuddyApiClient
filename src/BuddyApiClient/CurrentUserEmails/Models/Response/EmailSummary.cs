namespace BuddyApiClient.CurrentUserEmails.Models.Response
{
    using System.Text.Json.Serialization;

    public sealed record EmailSummary
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("confirmed")]
        public bool Confirmed { get; set; }
    }
}