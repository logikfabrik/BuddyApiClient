namespace BuddyApiClient.Workspaces.Models.Response;

using System.Text.Json.Serialization;
using BuddyApiClient.Core.Models;

public sealed record Workspace : ApiObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("domain")]
    public string? Domain { get; set; }
}
