namespace BuddyApiClient.IntegrationTest.Pipelines.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Pipelines.Models.Request;

    internal static class CreateOnEventPipelineRequestFactory
    {
        private static readonly Faker<CreateOnEventPipeline> Faker = new Faker<CreateOnEventPipeline>().CustomInstantiator(f => new CreateOnEventPipeline(f.Lorem.Word(), new[] { new Event(f.Random.Enum<EventType>(), new []{ "refs/heads/master" }) }));

        public static CreateOnEventPipeline Create()
        {
            return Faker.Generate();
        }
    }
}
