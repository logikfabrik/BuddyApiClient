namespace BuddyApiClient.IntegrationTest.Projects.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Projects.Models.Request;

    internal static class UpdateProjectRequestFactory
    {
        private static readonly Faker<UpdateProject> Faker = new Faker<UpdateProject>().RuleFor(model => model.DisplayName, f => f.Lorem.Word());

        public static UpdateProject Create()
        {
            return Faker.Generate();
        }
    }
}