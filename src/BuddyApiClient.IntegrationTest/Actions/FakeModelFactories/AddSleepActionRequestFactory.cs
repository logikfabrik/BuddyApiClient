namespace BuddyApiClient.IntegrationTest.Actions.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Actions.Models.Request;

    internal static class AddSleepActionRequestFactory
    {
        private static readonly Faker<AddSleepAction> Faker = new Faker<AddSleepAction>().CustomInstantiator(f => new AddSleepAction(f.Lorem.Word()));

        public static AddSleepAction Create()
        {
            return Faker.Generate();
        }
    }
}