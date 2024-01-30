namespace BuddyApiClient.IntegrationTest.Pipelines.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Pipelines.Models.Request;

    internal static class CreateOnClickPipelineRequestFactory
    {
        private static readonly Faker<CreateOnClickPipeline> s_faker = new Faker<CreateOnClickPipeline>().CustomInstantiator(f => new CreateOnClickPipeline(f.Lorem.Word()));

        public static CreateOnClickPipeline Create()
        {
            return s_faker.Generate();
        }
    }
}
