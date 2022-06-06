namespace BuddyApiClient.IntegrationTest.PermissionSets.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.PermissionSets.Models.Request;

    internal static class CreatePermissionSetRequestFactory
    {
        private static readonly Faker<CreatePermissionSet> Faker = new Faker<CreatePermissionSet>().CustomInstantiator(f => new CreatePermissionSet(f.Lorem.Word()));

        public static CreatePermissionSet Create()
        {
            return Faker.Generate();
        }
    }
}