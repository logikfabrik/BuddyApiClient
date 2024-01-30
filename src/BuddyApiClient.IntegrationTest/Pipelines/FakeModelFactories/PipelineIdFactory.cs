namespace BuddyApiClient.IntegrationTest.Pipelines.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Pipelines.Models;

    internal static class PipelineIdFactory
    {
        private static readonly Randomizer s_randomizer = new();

        public static PipelineId Create()
        {
            return new PipelineId(s_randomizer.Int(999, 9999));
        }
    }
}
