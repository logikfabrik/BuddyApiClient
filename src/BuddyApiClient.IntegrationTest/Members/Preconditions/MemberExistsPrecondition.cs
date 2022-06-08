namespace BuddyApiClient.IntegrationTest.Members.Preconditions
{
    using System.Net;
    using BuddyApiClient.IntegrationTest.Members.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.Members;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class MemberExistsPrecondition : Precondition<MemberId>
    {
        public MemberExistsPrecondition(IMembersClient client, Func<Task<Domain>> domainSetUp) : base(SetUp(client, domainSetUp), setUp => TearDown(client, domainSetUp, setUp))
        {
        }

        private static Func<Task<MemberId>> SetUp(IMembersClient client, Func<Task<Domain>> domainSetUp)
        {
            return async () =>
            {
                var member = await client.Add(await domainSetUp(), AddMemberRequestFactory.Create());

                return member?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IMembersClient client, Func<Task<Domain>> domainSetUp, Func<Task<MemberId>> setUp)
        {
            return async () =>
            {
                try
                {
                    await client.Remove(await domainSetUp(), await setUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing.
                }
            };
        }
    }
}