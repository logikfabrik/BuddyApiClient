namespace BuddyApiClient.IntegrationTest.CurrentUserEmails.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.CurrentUserEmails.Models.Request;

    internal static class AddInvalidEmailRequestFactory
    {
        private static readonly Faker<AddEmail> Faker = new Faker<AddEmail>().CustomInstantiator(f => new AddEmail(f.Lorem.Word()));

        public static AddEmail Create()
        {
            return Faker.Generate();
        }
    }
}
