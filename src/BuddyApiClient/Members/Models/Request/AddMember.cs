namespace BuddyApiClient.Members.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record AddMember
    {
        public AddMember(string email)
        {
            Email = Ensure.String.IsNotNullOrEmpty(email, nameof(email));
        }

        [JsonPropertyName("email")]
        public string Email { get; }
    }
}