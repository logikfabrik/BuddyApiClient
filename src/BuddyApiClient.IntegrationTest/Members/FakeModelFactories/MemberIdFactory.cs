namespace BuddyApiClient.IntegrationTest.Members.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Members.Models;

    internal static class MemberIdFactory
    {
        private static readonly Randomizer s_randomizer = new();

        public static MemberId Create()
        {
            return new MemberId(s_randomizer.Int(999, 9999));
        }
    }
}