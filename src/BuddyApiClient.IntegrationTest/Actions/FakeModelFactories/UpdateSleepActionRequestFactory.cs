namespace BuddyApiClient.IntegrationTest.Actions.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Actions.Models.Request;

    internal static class UpdateSleepActionRequestFactory
    {
        private static readonly Faker<UpdateSleepAction> Faker = new Faker<UpdateSleepAction>()
            .RuleFor(model => model.Name, f => f.Lorem.Word())
            .RuleFor(model => model.SleepInSeconds, f => f.Random.Number(SleepAction.MaxSleepInSeconds, SleepAction.MaxSleepInSeconds));

        public static UpdateSleepAction Create()
        {
            return Faker.Generate();
        }
    }
}
