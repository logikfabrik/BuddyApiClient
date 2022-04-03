namespace BuddyApiClient.IntegrationTest.Testing.Preconditions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.Members;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class MemberExistsPrecondition : Precondition<MemberId>
    {
        public MemberExistsPrecondition(IMembersClient client, Func<Task<Domain>> domainSetUp, string email) : base(SetUp(client, domainSetUp, email), setUp => TearDown(client, domainSetUp, setUp))
        {
        }

        private static Func<Task<MemberId>> SetUp(IMembersClient client, Func<Task<Domain>> domainSetUp, string email)
        {
            return async () =>
            {
                var member = await client.Add(await domainSetUp(), new AddMember(email));

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
                    // Do nothing
                }
            };
        }
    }
}