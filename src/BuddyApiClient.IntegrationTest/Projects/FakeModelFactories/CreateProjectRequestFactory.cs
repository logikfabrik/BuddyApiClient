namespace BuddyApiClient.IntegrationTest.Projects.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Projects.Models.Request;

    internal static class CreateProjectRequestFactory
    {
        private static readonly Faker<CreateProject> s_faker = new Faker<CreateProject>().CustomInstantiator(f => new CreateProject(f.Lorem.Word()));

        public static CreateProject Create()
        {
            return s_faker.Generate();
        }
    }
}