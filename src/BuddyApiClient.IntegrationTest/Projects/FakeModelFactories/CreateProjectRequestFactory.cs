namespace BuddyApiClient.IntegrationTest.Projects.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Projects.Models.Request;

    internal static class CreateProjectRequestFactory
    {
        private static readonly Faker<CreateProject> Faker = new Faker<CreateProject>().CustomInstantiator(f => new CreateProject(f.Lorem.Word()));

        public static CreateProject Create()
        {
            return Faker.Generate();
        }
    }
}