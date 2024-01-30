namespace BuddyApiClient.IntegrationTest.Variables.Preconditions
{
    using System.Net;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.IntegrationTest.Variables.FakeModelFactories;
    using BuddyApiClient.Variables;
    using BuddyApiClient.Variables.Models;
    using BuddyApiClient.Variables.Models.Request;

    internal class VariableExistsPrecondition : Precondition<VariableId>
    {
        public VariableExistsPrecondition(IVariablesClient client, Func<Task<BuddyApiClient.Workspaces.Models.Domain>> domainSetUp) : this(client, domainSetUp, CreateVariableRequestFactory.Create)
        {
        }

        protected VariableExistsPrecondition(IVariablesClient client, Func<Task<BuddyApiClient.Workspaces.Models.Domain>> domainSetUp, Func<CreateVariable> createVariableFactory) : base(SetUp(client, domainSetUp, createVariableFactory), setUp => TearDown(client, domainSetUp, setUp))
        {
        }

        private static Func<Task<VariableId>> SetUp(IVariablesClient client, Func<Task<BuddyApiClient.Workspaces.Models.Domain>> domainSetUp, Func<CreateVariable> createVariableFactory)
        {
            return async () =>
            {
                var variable = await client.Create(await domainSetUp(), createVariableFactory());

                return variable?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IVariablesClient client, Func<Task<BuddyApiClient.Workspaces.Models.Domain>> domainSetUp, Func<Task<VariableId>> setUp)
        {
            return async () =>
            {
                try
                {
                    await client.Delete(await domainSetUp(), await setUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing.
                }
            };
        }
    }
}