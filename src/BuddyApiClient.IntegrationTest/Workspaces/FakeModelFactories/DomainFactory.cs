namespace BuddyApiClient.IntegrationTest.Workspaces.FakeModelFactories
{
    using Bogus.DataSets;
    using BuddyApiClient.Workspaces.Models;

    internal static class DomainFactory
    {
        private static readonly Lorem s_lorem = new();

        public static Domain Create()
        {
            return new Domain(s_lorem.Word());
        }
    }
}