namespace BuddyApiClient.IntegrationTest.Actions.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Actions.Models.Request;

    internal static class UpdateSleepActionRequestFactory
    {
        private static readonly Faker<UpdateSleepAction> s_faker = new Faker<UpdateSleepAction>()
            .RuleFor(model => model.Name, f => f.Lorem.Word())
            .RuleFor(model => model.SleepInSeconds, f => f.Random.Int(SleepAction.MaxSleepInSeconds, SleepAction.MaxSleepInSeconds));

        public static UpdateSleepAction Create()
        {
            return s_faker.Generate();
        }
    }
}
