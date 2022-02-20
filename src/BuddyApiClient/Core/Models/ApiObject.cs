namespace BuddyApiClient.Core.Models;

using System;
using System.Text.Json.Serialization;

public abstract record ApiObject
{
    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("html_url")]
    public Uri? HtmlUrl { get; set; }
}
