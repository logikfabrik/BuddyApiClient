namespace BuddyApiClient.IntegrationTest.Projects.FakeModelFactories
{
    using Bogus.DataSets;
    using BuddyApiClient.Projects.Models;

    internal static class ProjectNameFactory
    {
        private static readonly Lorem s_lorem = new();

        public static ProjectName Create()
        {
            return new ProjectName(s_lorem.Word());
        }
    }
}