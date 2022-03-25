namespace BuddyApiClient.IntegrationTest
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class DomainExistsPrecondition : Precondition<string>
    {
        public DomainExistsPrecondition(IBuddyClient client) : base(Arrange(client))
        {
        }

        private static Func<Task<string>> Arrange(IBuddyClient client)
        {
            return async () =>
            {
                var workspaces = (await client.Workspaces.List())?.Workspaces.ToArray();

                var domain = workspaces?.FirstOrDefault()?.Domain;

                return domain ?? throw new Exception();
            };
        }
    }
}