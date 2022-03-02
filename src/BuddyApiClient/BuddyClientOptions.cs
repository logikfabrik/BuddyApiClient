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
        ///     A access token.
        /// </summary>
        public string? AccessToken { get; set; }
    }
}