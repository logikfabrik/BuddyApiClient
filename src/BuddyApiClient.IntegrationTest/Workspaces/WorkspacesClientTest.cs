﻿namespace BuddyApiClient.IntegrationTest.Workspaces
{
    using System;
    using System.Threading.Tasks;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using FluentAssertions;
    using Xunit;

    public sealed class WorkspacesClientTest
    {
        public sealed class Get : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Get(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Return_The_Workspace_If_It_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Workspaces;

                var workspace = await sut.Get(await domain());

                workspace.Should().NotBeNull();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Return_Workspaces()
            {
                var sut = Fixture.BuddyClient.Workspaces;

                var workspaces = await sut.List();

                workspaces?.Workspaces.Should().NotBeEmpty();
            }
        }
    }
}