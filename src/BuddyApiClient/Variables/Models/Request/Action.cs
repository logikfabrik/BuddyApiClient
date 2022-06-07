namespace BuddyApiClient.Variables.Models.Request
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Actions.Models;

    public sealed record Action : IScope
    {
        [JsonPropertyName("id")]
        public ActionId Id { get; set; }
    }
}