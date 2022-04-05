﻿namespace BuddyApiClient.IntegrationTest.Testing
{
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using Xunit;

    [Collection(nameof(BuddyClientCollection))]
    public abstract class BuddyClientTest : IntegrationTest
    {
        protected BuddyClientTest(BuddyClientFixture fixture)
        {
            Fixture = fixture;
        }

        protected BuddyClientFixture Fixture { get; }
    }
}