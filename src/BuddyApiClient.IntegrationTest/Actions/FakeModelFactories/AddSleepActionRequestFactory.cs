namespace BuddyApiClient.IntegrationTest.Actions.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Actions.Models.Request;

    internal static class AddSleepActionRequestFactory
    {
        private static readonly Faker<AddSleepAction> s_faker = new Faker<AddSleepAction>()
            .CustomInstantiator(f => new AddSleepAction(f.Lorem.Word()))
            .RuleFor(model => model.SleepInSeconds, f => f.Random.Int(SleepAction.MinSleepInSeconds, SleepAction.MaxSleepInSeconds));

        public static AddSleepAction Create()
        {
            return s_faker.Generate();
        }
    }
}