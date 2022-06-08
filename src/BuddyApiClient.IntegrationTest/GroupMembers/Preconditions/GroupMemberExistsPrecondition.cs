namespace BuddyApiClient.IntegrationTest.GroupMembers.Preconditions
{
    using System.Net;
    using BuddyApiClient.GroupMembers;
    using BuddyApiClient.GroupMembers.Models.Request;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class GroupMemberExistsPrecondition : Precondition<MemberId>
    {
        public GroupMemberExistsPrecondition(IGroupMembersClient client, Func<Task<Domain>> domainSetUp, Func<Task<GroupId>> groupSetUp, Func<Task<MemberId>> memberSetUp) : base(SetUp(client, domainSetUp, groupSetUp, memberSetUp), setUp => TearDown(client, domainSetUp, groupSetUp, setUp))
        {
        }

        private static Func<Task<MemberId>> SetUp(IGroupMembersClient client, Func<Task<Domain>> domainSetUp, Func<Task<GroupId>> groupSetUp, Func<Task<MemberId>> memberSetUp)
        {
            return async () =>
            {
                var member = await client.Add(await domainSetUp(), await groupSetUp(), new AddGroupMember { MemberId = await memberSetUp() });

                return member?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IGroupMembersClient client, Func<Task<Domain>> domainSetUp, Func<Task<GroupId>> groupSetUp, Func<Task<MemberId>> setUp)
        {
            return async () =>
            {
                try
                {
                    await client.Remove(await domainSetUp(), await groupSetUp(), await setUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing.
                }
            };
        }
    }
}