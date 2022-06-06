namespace BuddyApiClient.IntegrationTest.PermissionSets.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.PermissionSets.Models.Request;

    internal static class UpdatePermissionSetRequestFactory
    {
        private static readonly Faker<UpdatePermissionSet> Faker = new Faker<UpdatePermissionSet>().RuleFor(model => model.Name, f => f.Lorem.Word());

        public static UpdatePermissionSet Create()
        {
            return Faker.Generate();
        }
    }
}
