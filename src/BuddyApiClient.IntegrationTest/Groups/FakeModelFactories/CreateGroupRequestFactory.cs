namespace BuddyApiClient.IntegrationTest.Groups.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Groups.Models.Request;

    internal static class CreateGroupRequestFactory
    {
        private static readonly Faker<CreateGroup> s_faker = new Faker<CreateGroup>().CustomInstantiator(f => new CreateGroup(f.Lorem.Word())).RuleFor(model => model.Description, f => f.Lorem.Slug());

        public static CreateGroup Create()
        {
            return s_faker.Generate();
        }
    }
}