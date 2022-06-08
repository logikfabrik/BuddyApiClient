namespace BuddyApiClient.IntegrationTest.Variables.Preconditions
{
    using BuddyApiClient.IntegrationTest.Variables.FakeModelFactories;
    using BuddyApiClient.Variables;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class SshKeyVariableExistsPrecondition : VariableExistsPrecondition
    {
        public SshKeyVariableExistsPrecondition(IVariablesClient client, Func<Task<Domain>> domainSetUp) : base(client, domainSetUp, CreateSshKeyVariableRequestFactory.Create)
        {
        }
    }
}