﻿namespace BuddyApiClient.IntegrationTest.Workspaces.Preconditions
{
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.Workspaces;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class DomainExistsPrecondition : Precondition<Domain>
    {
        public DomainExistsPrecondition(IWorkspacesClient client) : base(SetUp(client), _ => () => Task.CompletedTask)
        {
        }

        private static Func<Task<Domain>> SetUp(IWorkspacesClient client)
        {
            return async () =>
            {
                var workspaces = (await client.List())?.Workspaces.ToArray();

                var domain = workspaces?.FirstOrDefault()?.Domain;

                return domain ?? throw new PreconditionSetUpException();
            };
        }
    }
}