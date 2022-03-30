namespace BuddyApiClient.Projects.Models.Request
{
    using EnsureThat;

    public sealed record Integration
    {
        public Integration(string hashId)
        {
            HashId = Ensure.String.IsNotNullOrEmpty(hashId);
        }

        public string HashId { get; }
    }
}