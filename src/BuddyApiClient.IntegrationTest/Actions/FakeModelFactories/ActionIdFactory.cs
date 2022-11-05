namespace BuddyApiClient.IntegrationTest.Actions.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Actions.Models;

    internal static class ActionIdFactory
    {
        private static readonly Randomizer Randomizer = new();

        public static ActionId Create()
        {
            return new ActionId(Randomizer.Int(999, 9999));
        }
    }
}
