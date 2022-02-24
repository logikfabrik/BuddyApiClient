﻿namespace BuddyApiClient.Members.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record MemberDetails : Response
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("avatar_url")]
        public Uri? AvatarUrl { get; set; }

        [JsonPropertyName("admin")]
        public bool Admin { get; set; }

        [JsonPropertyName("workspace_owner")]
        public bool WorkspaceOwner { get; set; }
    }
}