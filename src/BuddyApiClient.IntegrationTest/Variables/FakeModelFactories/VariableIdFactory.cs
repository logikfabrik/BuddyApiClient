namespace BuddyApiClient.IntegrationTest.Variables.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Variables.Models;

    internal static class VariableIdFactory
    {
        private static readonly Randomizer Randomizer = new();

        public static VariableId Create()
        {
            return new VariableId(Randomizer.Int(999, 9999));
        }
    }
}
