namespace BuddyApiClient.IntegrationTest
{
    using System;
    using System.Threading.Tasks;
    using BuddyApiClient.PermissionSets.Models.Request;

    public sealed class PermissionSetExistsPrecondition : Precondition<int>
    {
        private readonly Func<Task> _dispose;

        private bool _disposed;

        public PermissionSetExistsPrecondition(IBuddyClient client, Precondition<string> domainExistsPrecondition, string name) : base(Arrange(client, domainExistsPrecondition, name))
        {
            _dispose = async () => { await client.PermissionSets.Delete(await domainExistsPrecondition.Arrange(), await Arrange()); };
        }

        private static Func<Task<int>> Arrange(IBuddyClient client, Precondition<string> domainExistsPrecondition, string name)
        {
            return async () =>
            {
                var permissionSet = await client.PermissionSets.Create(await domainExistsPrecondition.Arrange(), new CreatePermissionSet(name));

                return permissionSet?.Id ?? throw new Exception();
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