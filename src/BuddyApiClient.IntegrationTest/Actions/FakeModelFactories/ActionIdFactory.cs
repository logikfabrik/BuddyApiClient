namespace BuddyApiClient.IntegrationTest.Actions.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Actions.Models;

    internal static class ActionIdFactory
    {
        private static readonly Randomizer s_randomizer = new();

        public static ActionId Create()
        {
            return new ActionId(s_randomizer.Int(999, 9999));
        }
    }
}
