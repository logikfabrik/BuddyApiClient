namespace BuddyApiClient.IntegrationTest
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal sealed class Preconditions
    {
        private static readonly Lazy<Preconditions> Lazy = new(() => new Preconditions());

        private readonly IReadOnlyCollection<Precondition> _preconditions = new ConcurrentBag<Precondition>();

        private Preconditions()
        {
        }

        public static Preconditions Instance => Lazy.Value;
    }
}