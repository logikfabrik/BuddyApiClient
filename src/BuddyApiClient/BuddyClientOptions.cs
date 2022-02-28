namespace BuddyApiClient
{
    public sealed record BuddyClientOptions
    {
        /// <summary>
        ///     The API base URL. Should be https://api.buddy.works or https://YOUR-IP-ADDRESS/api for Buddy Enterprise
        ///     (on-premises).
        /// </summary>
        public Uri BaseUrl { get; set; } = new("https://api.buddy.works");

        /// <summary>
        ///     A OAuth2 or personal access token.
        /// </summary>
        public string? AccessToken { get; set; }

        public string? BasicAuthClientId { get; set; }

        public string? BasicAuthClientSecret { get; set; }

        internal bool UseAccessToken => AccessToken is not null;

        internal bool UseBasicAuth => BasicAuthClientId is not null && BasicAuthClientSecret is not null;
    }
}