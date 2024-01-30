namespace BuddyApiClient.IntegrationTest.Groups.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Groups.Models;

    internal static class GroupIdFactory
    {
        private static readonly Randomizer s_randomizer = new();

        public static GroupId Create()
        {
            return new GroupId(s_randomizer.Int(999, 9999));
        }
    }
}