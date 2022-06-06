namespace BuddyApiClient.IntegrationTest.Variables.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Variables.Models.Request;

    internal static class CreateVariableRequestFactory
    {
        private static readonly Faker<CreateVariable> Faker = new Faker<CreateVariable>().CustomInstantiator(f => new CreateVariable(f.Lorem.Word(), f.Lorem.Word()));

        public static CreateVariable Create()
        {
            return Faker.Generate();
        }
    }
}