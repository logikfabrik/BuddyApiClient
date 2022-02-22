﻿namespace BuddyApiClient.Members.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public record AddMember
    {
        public AddMember(string email)
        {
            Email = Ensure.String.IsNotNullOrWhiteSpace(email, nameof(email));
        }

        [JsonPropertyName("email")]
        public string Email { get; }
    }
}