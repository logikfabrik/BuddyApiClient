namespace BuddyApiClient.IntegrationTest.CurrentUserEmails.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.CurrentUserEmails.Models.Request;

    internal static class AddEmailRequestFactory
    {
        private static readonly Faker<AddEmail> s_faker = new Faker<AddEmail>().CustomInstantiator(f => new AddEmail(f.Internet.ExampleEmail()));

        public static AddEmail Create()
        {
            return s_faker.Generate();
        }
    }
}