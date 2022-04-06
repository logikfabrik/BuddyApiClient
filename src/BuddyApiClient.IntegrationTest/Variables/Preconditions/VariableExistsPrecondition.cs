namespace BuddyApiClient.IntegrationTest.Variables.Preconditions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.IntegrationTest.Variables.FakeModelFactories;
    using BuddyApiClient.Variables;
    using BuddyApiClient.Variables.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class VariableExistsPrecondition : Precondition<VariableId>
    {
        public VariableExistsPrecondition(IVariablesClient client, Func<Task<Domain>> domainSetUp) : base(SetUp(client, domainSetUp), setUp => TearDown(client, domainSetUp, setUp))
        {
        }

        private static Func<Task<VariableId>> SetUp(IVariablesClient client, Func<Task<Domain>> domainSetUp)
        {
            return async () =>
            {
                var variable = await client.Create(await domainSetUp(), CreateVariableFactory.Create());

                return variable?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IVariablesClient client, Func<Task<Domain>> domainSetUp, Func<Task<VariableId>> setUp)
        {
            return async () =>
            {
                try
                {
                    await client.Delete(await domainSetUp(), await setUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing
                }
            };
        }
    }
}