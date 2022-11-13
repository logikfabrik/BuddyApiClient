namespace BuddyApiClient.IntegrationTest.Pipelines.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Pipelines.Models;

    internal static class PipelineIdFactory
    {
        private static readonly Randomizer Randomizer = new();

        public static PipelineId Create()
        {
            return new PipelineId(Randomizer.Int(999, 9999));
        }
    }
}
