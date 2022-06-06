namespace BuddyApiClient.IntegrationTest.Projects.FakeModelFactories
{
    using Bogus.DataSets;
    using BuddyApiClient.Projects.Models;

    internal static class ProjectNameFactory
    {
        private static readonly Lorem Lorem = new();

        public static ProjectName Create()
        {
            return new ProjectName(Lorem.Word());
        }
    }
}
