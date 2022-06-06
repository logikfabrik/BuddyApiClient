namespace BuddyApiClient.IntegrationTest.PermissionSets.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.PermissionSets.Models;

    internal static class PermissionSetIdFactory
    {
        private static readonly Randomizer Randomizer = new();

        public static PermissionSetId Create()
        {
            return new PermissionSetId(Randomizer.Int(999, 9999));
        }
    }
}
