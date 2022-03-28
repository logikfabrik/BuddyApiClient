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
        private readonly Func<Task> _tearDown;

        public PermissionSetExistsPrecondition(IPermissionSetsClient client, Precondition<Domain> domainExistsPrecondition, string name) : base(SetUp(client, domainExistsPrecondition, name))
        {
            _tearDown = async () =>
            {
                try
                {
                    await client.Delete(await domainExistsPrecondition.SetUp(), await SetUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing
                }
            };
        }

        private static Func<Task<PermissionSetId>> SetUp(IPermissionSetsClient client, Precondition<Domain> domainExistsPrecondition, string name)
        {
            return async () =>
            {
                var permissionSet = await client.Create(await domainExistsPrecondition.SetUp(), new CreatePermissionSet(name));

                return permissionSet?.Id ?? throw new PreconditionSetUpException();
            };
        }

        public override async Task TearDown()
        {
            await base.TearDown();

            if (!IsSetUp)
            {
                return;
            }

            await _tearDown();
        }
    }
}