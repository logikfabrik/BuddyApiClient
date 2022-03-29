namespace BuddyApiClient.IntegrationTest
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.PermissionSets.Models.Request;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class PermissionSetExistsPrecondition : Precondition<PermissionSetId>
    {
        public PermissionSetExistsPrecondition(IPermissionSetsClient client, Func<Task<Domain>> domainSetUp, string name) : base(SetUp(client, domainSetUp, name), setUp => TearDown(client, domainSetUp, setUp))
        {
        }

        private static Func<Task<PermissionSetId>> SetUp(IPermissionSetsClient client, Func<Task<Domain>> domainSetUp, string name)
        {
            return async () =>
            {
                var permissionSet = await client.Create(await domainSetUp(), new CreatePermissionSet(name));

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
                    // Do nothing
                }
            };
        }
    }
}