namespace BuddyApiClient.IntegrationTest.CurrentUserEmails.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.CurrentUserEmails.Models.Request;

    internal static class AddEmailFactory
    {
        private static readonly Faker<AddEmail> Faker = new Faker<AddEmail>().CustomInstantiator(f => new AddEmail(f.Internet.ExampleEmail()));

        public static AddEmail Create()
        {
            return Faker.Generate();
        }
    }
}