namespace BuddyApiClient.IntegrationTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Preconditions
    {
        private readonly ISet<Precondition> _preconditions;

        public Preconditions()
        {
            _preconditions = new HashSet<Precondition>();
        }

        public Preconditions Add(Precondition preconditionToAdd)
        {
            _preconditions.Add(preconditionToAdd);

            return this;
        }

        public Preconditions Add<T>(Precondition<T> preconditionToAdd, out Precondition<T> precondition, out Func<Task<T>> setUp) where T : struct
        {
            _preconditions.Add(preconditionToAdd);

            precondition = preconditionToAdd;
            setUp = preconditionToAdd.SetUp;

            return this;
        }

        public async Task SetUp()
        {
            await Task.WhenAll(_preconditions.Select(precondition => precondition.SetUp()));
        }

        public async Task TearDown()
        {
            await Task.WhenAll(_preconditions.Select(precondition => precondition.TearDown()));
        }
    }
}