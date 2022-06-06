namespace BuddyApiClient.IntegrationTest.Variables.Preconditions
{
    using System;
    using System.Threading.Tasks;
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