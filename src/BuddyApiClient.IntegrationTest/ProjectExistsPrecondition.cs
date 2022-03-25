namespace BuddyApiClient.IntegrationTest
{
    using System;
    using System.Threading.Tasks;
    using BuddyApiClient.Projects.Models.Request;

    public sealed class ProjectExistsPrecondition : Precondition<string>
    {
        private readonly Func<Task> _dispose;

        private bool _disposed;

        public ProjectExistsPrecondition(IBuddyClient client, Precondition<string> domainExistsPrecondition, string displayName) : base(Arrange(client, domainExistsPrecondition, displayName))
        {
            _dispose = async () => { await client.Projects.Delete(await domainExistsPrecondition.Arrange(), await Arrange()); };
        }

        private static Func<Task<string>> Arrange(IBuddyClient client, Precondition<string> domainExistsPrecondition, string displayName)
        {
            return async () =>
            {
                var project = await client.Projects.Create(await domainExistsPrecondition.Arrange(), new CreateProject(displayName));

                return project?.Name ?? throw new Exception();
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