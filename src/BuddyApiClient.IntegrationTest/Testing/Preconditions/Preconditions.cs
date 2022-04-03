namespace BuddyApiClient.IntegrationTest.Testing.Preconditions
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Preconditions
    {
        private readonly ConcurrentStack<Precondition> _preconditions;

        public Preconditions()
        {
            _preconditions = new ConcurrentStack<Precondition>();
        }

        public Preconditions Add(Precondition precondition)
        {
            _preconditions.Push(precondition);

            return this;
        }

        public Preconditions Add<T>(Precondition<T> precondition, out Func<Task<T>> setUp) where T : struct
        {
            setUp = precondition.SetUp;

            return Add(precondition);
        }

        public async Task SetUp()
        {
            await Task.WhenAll(_preconditions.Select(precondition => precondition.SetUp()));
        }

        public async IAsyncEnumerable<Precondition> TearDown()
        {
            while (!_preconditions.IsEmpty)
            {
                if (!_preconditions.TryPop(out var precondition))
                {
                    continue;
                }

                await precondition.TearDown();

                yield return precondition;
            }
        }
    }
}