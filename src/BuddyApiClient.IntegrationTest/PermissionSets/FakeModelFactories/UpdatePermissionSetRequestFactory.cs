namespace BuddyApiClient.IntegrationTest.PermissionSets.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.PermissionSets.Models.Request;

    internal static class UpdatePermissionSetRequestFactory
    {
        private static readonly Faker<UpdatePermissionSet> s_faker = new Faker<UpdatePermissionSet>().RuleFor(model => model.Name, f => f.Lorem.Word());

        public static UpdatePermissionSet Create()
        {
            return s_faker.Generate();
        }
    }
}