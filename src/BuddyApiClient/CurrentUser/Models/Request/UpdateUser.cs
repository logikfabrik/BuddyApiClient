namespace BuddyApiClient.CurrentUser.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record UpdateUser
    {
        public UpdateUser(string name)
        {
            Name = Ensure.String.IsNotNullOrEmpty(name, nameof(name));
        }

        [JsonPropertyName("name")]
        public string Name { get; }
    }
}