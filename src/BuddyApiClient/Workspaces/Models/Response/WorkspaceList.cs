namespace BuddyApiClient.Workspaces.Models.Response;

using System.Collections.Generic;
using System.Text.Json.Serialization;
using BuddyApiClient.Core.Models;

public sealed record WorkspaceList : ApiObject
{
    [JsonPropertyName("workspaces")]
    public IEnumerable<Workspace>? Workspaces { get; set; }
}
