namespace BuddyApiClient.Groups.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record CreateGroup
    {
        public CreateGroup(string name)
        {
            Name = Ensure.String.IsNotNullOrEmpty(name, nameof(name));
        }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}