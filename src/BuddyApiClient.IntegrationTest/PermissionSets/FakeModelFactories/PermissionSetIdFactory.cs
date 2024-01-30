namespace BuddyApiClient.IntegrationTest.PermissionSets.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.PermissionSets.Models;

    internal static class PermissionSetIdFactory
    {
        private static readonly Randomizer s_randomizer = new();

        public static PermissionSetId Create()
        {
            return new PermissionSetId(s_randomizer.Int(999, 9999));
        }
    }
}