namespace BuddyApiClient.IntegrationTest.Groups.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Groups.Models.Request;

    internal static class UpdateGroupRequestFactory
    {
        private static readonly Faker<UpdateGroup> s_faker = new Faker<UpdateGroup>().RuleFor(model => model.Name, f => f.Lorem.Word());

        public static UpdateGroup Create()
        {
            return s_faker.Generate();
        }
    }
}