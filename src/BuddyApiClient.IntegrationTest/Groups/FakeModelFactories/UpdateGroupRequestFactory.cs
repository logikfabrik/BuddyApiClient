namespace BuddyApiClient.IntegrationTest.Groups.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Groups.Models.Request;

    internal static class UpdateGroupRequestFactory
    {
        private static readonly Faker<UpdateGroup> Faker = new Faker<UpdateGroup>().RuleFor(model => model.Name, f => f.Lorem.Word());

        public static UpdateGroup Create()
        {
            return Faker.Generate();
        }
    }
}