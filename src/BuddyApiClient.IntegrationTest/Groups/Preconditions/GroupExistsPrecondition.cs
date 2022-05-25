namespace BuddyApiClient.IntegrationTest.Groups.Preconditions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.Groups;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.IntegrationTest.Groups.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class GroupExistsPrecondition : Precondition<GroupId>
    {
        public GroupExistsPrecondition(IGroupsClient client, Func<Task<Domain>> domainSetUp) : base(SetUp(client, domainSetUp), setUp => TearDown(client, domainSetUp, setUp))
        {
        }

        private static Func<Task<GroupId>> SetUp(IGroupsClient client, Func<Task<Domain>> domainSetUp)
        {
            return async () =>
            {
                var group = await client.Create(await domainSetUp(), CreateGroupFactory.Create());

                return group?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IGroupsClient client, Func<Task<Domain>> domainSetUp, Func<Task<GroupId>> setUp)
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