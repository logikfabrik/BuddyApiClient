namespace BuddyApiClient.IntegrationTest.Pipelines.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Pipelines.Models.Request;

    internal static class CreateOnSchedulePipelineRequestFactory
    {
        private static readonly Faker<CreateOnSchedulePipeline> s_faker = new Faker<CreateOnSchedulePipeline>().CustomInstantiator(f => new CreateOnSchedulePipeline(f.Lorem.Word(), f.Date.Future(), f.Random.Int(min: 1, max: 5)));

        public static CreateOnSchedulePipeline Create()
        {
            return s_faker.Generate();
        }
    }
}
