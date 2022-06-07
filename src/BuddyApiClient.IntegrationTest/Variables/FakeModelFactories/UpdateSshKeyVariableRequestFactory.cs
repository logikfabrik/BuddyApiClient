namespace BuddyApiClient.IntegrationTest.Variables.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Variables.Models.Request;

    internal static class UpdateSshKeyVariableRequestFactory
    {
        private static readonly Faker<UpdateSshKeyVariable> Faker = new Faker<UpdateSshKeyVariable>().RuleFor(model => model.Description, f => f.Lorem.Word());

        public static UpdateSshKeyVariable Create()
        {
            return Faker.Generate();
        }
    }
}