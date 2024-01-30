namespace BuddyApiClient.IntegrationTest.Variables.FakeModelFactories
{
    using Bogus;
    using BuddyApiClient.Variables.Models;
    using BuddyApiClient.Variables.Models.Request;
    using SshKeyGenerator;

    internal static class CreateSshKeyVariableRequestFactory
    {
        private static readonly Faker<CreateSshKeyVariable> s_faker = new Faker<CreateSshKeyVariable>().CustomInstantiator(f =>
        {
            using var keyGenerator = new SshKeyGenerator(1024);

            return new CreateSshKeyVariable(f.Lorem.Word(), keyGenerator.ToPrivateKey(), f.PickRandom<FilePlace>(), f.System.FileName(), f.System.FilePath(), new FilePermission("600"));
        });

        public static CreateSshKeyVariable Create()
        {
            return s_faker.Generate();
        }
    }
}