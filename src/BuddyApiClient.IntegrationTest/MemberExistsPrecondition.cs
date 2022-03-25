namespace BuddyApiClient.IntegrationTest
{
    using System;
    using System.Threading.Tasks;
    using BuddyApiClient.Members.Models.Request;

    public sealed class MemberExistsPrecondition : Precondition<int>
    {
        private readonly Func<Task> _dispose;

        private bool _disposed;

        public MemberExistsPrecondition(IBuddyClient client, Precondition<string> domainExistsPrecondition, string email) : base(Arrange(client, domainExistsPrecondition, email))
        {
            _dispose = async () => { await client.Members.Remove(await domainExistsPrecondition.Arrange(), await Arrange()); };
        }

        private static Func<Task<int>> Arrange(IBuddyClient client, Precondition<string> domainExistsPrecondition, string email)
        {
            return async () =>
            {
                var member = await client.Members.Add(await domainExistsPrecondition.Arrange(), new AddMember(email));

                return member?.Id ?? throw new Exception();
            };
        }

        protected override async ValueTask Dispose(bool disposing)
        {
            await base.Dispose(disposing);

            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (!HasBeenArranged)
            {
                return;
            }

            await _dispose();
        }
    }
}