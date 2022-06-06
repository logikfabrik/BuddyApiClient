namespace BuddyApiClient.IntegrationTest.Workspaces.FakeModelFactories
{
    using Bogus.DataSets;
    using BuddyApiClient.Workspaces.Models;

    internal static class DomainFactory
    {
        private static readonly Lorem Lorem = new();

        public static Domain Create()
        {
            return new Domain(Lorem.Word());
        }
    }
}
