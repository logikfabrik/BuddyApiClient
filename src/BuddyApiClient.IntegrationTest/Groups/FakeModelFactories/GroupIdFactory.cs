namespace BuddyApiClient.IntegrationTest.Groups.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Groups.Models;

    internal static class GroupIdFactory
    {
        private static readonly Randomizer Randomizer = new();

        public static GroupId Create()
        {
            return new GroupId(Randomizer.Int(999, 9999));
        }
    }
}
