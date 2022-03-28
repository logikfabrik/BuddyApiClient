namespace BuddyApiClient.IntegrationTest
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
        private readonly Func<Task> _tearDown;

        public MemberExistsPrecondition(IMembersClient client, Precondition<Domain> domainExistsPrecondition, string email) : base(SetUp(client, domainExistsPrecondition, email))
        {
            _tearDown = async () =>
            {
                try
                {
                    await client.Remove(await domainExistsPrecondition.SetUp(), await SetUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing
                }
            };
        }

        private static Func<Task<MemberId>> SetUp(IMembersClient client, Precondition<Domain> domainExistsPrecondition, string email)
        {
            return async () =>
            {
                var member = await client.Add(await domainExistsPrecondition.SetUp(), new AddMember(email));

                return member?.Id ?? throw new PreconditionSetUpException();
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