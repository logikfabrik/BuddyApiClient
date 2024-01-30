namespace BuddyApiClient.IntegrationTest.Variables.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Variables.Models;

    internal static class VariableIdFactory
    {
        private static readonly Randomizer s_randomizer = new();

        public static VariableId Create()
        {
            return new VariableId(s_randomizer.Int(999, 9999));
        }
    }
}