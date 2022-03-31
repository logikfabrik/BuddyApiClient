namespace BuddyApiClient.Groups.Models.Request
{
    public sealed record UpdateGroup
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}