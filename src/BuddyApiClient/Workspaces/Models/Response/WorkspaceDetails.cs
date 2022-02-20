namespace BuddyApiClient.Workspaces.Models.Response;

using System;
using System.Text.Json.Serialization;
using BuddyApiClient.Core.Models;

public sealed record WorkspaceDetails : ApiObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("owner_id")]
    public int OwnerId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    [JsonPropertyName("frozen")]
    public bool Frozen { get; set; }

    [JsonPropertyName("create_date")]
    public DateTime CreateDate { get; set; }
}
