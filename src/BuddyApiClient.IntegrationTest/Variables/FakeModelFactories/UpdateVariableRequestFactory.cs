namespace BuddyApiClient.IntegrationTest.Variables.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Variables.Models.Request;

    internal static class UpdateVariableRequestFactory
    {
        private static readonly Faker<UpdateVariable> Faker = new Faker<UpdateVariable>().RuleFor(model => model.Description, f => f.Lorem.Word());

        public static UpdateVariable Create()
        {
            return Faker.Generate();
        }
    }
}
