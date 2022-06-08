namespace BuddyApiClient.IntegrationTest.PermissionSets.Preconditions
{
    using System.Net;
    using BuddyApiClient.IntegrationTest.PermissionSets.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class PermissionSetExistsPrecondition : Precondition<PermissionSetId>
    {
        public PermissionSetExistsPrecondition(IPermissionSetsClient client, Func<Task<Domain>> domainSetUp) : base(SetUp(client, domainSetUp), setUp => TearDown(client, domainSetUp, setUp))
        {
        }

        private static Func<Task<PermissionSetId>> SetUp(IPermissionSetsClient client, Func<Task<Domain>> domainSetUp)
        {
            return async () =>
            {
                var permissionSet = await client.Create(await domainSetUp(), CreatePermissionSetRequestFactory.Create());

                return permissionSet?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IPermissionSetsClient client, Func<Task<Domain>> domainSetUp, Func<Task<PermissionSetId>> setUp)
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