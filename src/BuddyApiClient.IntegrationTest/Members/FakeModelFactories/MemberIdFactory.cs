namespace BuddyApiClient.IntegrationTest.Members.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Members.Models;

    internal static class MemberIdFactory
    {
        private static readonly Randomizer Randomizer = new();

        public static MemberId Create()
        {
            return new MemberId(Randomizer.Int(999, 9999));
        }
    }
}