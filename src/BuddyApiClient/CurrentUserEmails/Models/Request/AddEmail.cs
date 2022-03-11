namespace BuddyApiClient.CurrentUserEmails.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record AddEmail
    {
        public AddEmail(string email)
        {
            Email = Ensure.String.IsNotNullOrEmpty(email, nameof(email));
        }

        [JsonPropertyName("email")]
        public string Email { get; }
    }
}