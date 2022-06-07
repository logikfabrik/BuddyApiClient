namespace BuddyApiClient.IntegrationTest.Members.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Members.Models.Request;

    internal static class AddMemberRequestFactory
    {
        private static readonly Faker<AddMember> Faker = new Faker<AddMember>().CustomInstantiator(f => new AddMember(f.Internet.ExampleEmail()));

        public static AddMember Create()
        {
            return Faker.Generate();
        }
    }
}