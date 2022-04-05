namespace BuddyApiClient.Variables.Models.Request
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Projects.Models;

    public sealed record Project : IScope
    {
        [JsonPropertyName("name")]
        public ProjectName Name { get; set; }
    }
}