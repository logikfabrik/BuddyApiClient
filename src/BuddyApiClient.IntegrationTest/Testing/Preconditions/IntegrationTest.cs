namespace BuddyApiClient.IntegrationTest.Testing.Preconditions
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public abstract class IntegrationTest : IAsyncLifetime
    {
        private readonly Lazy<Preconditions> _preconditions;

        protected IntegrationTest()
        {
            _preconditions = new Lazy<Preconditions>(() => new Preconditions());
        }

        protected Preconditions Preconditions => _preconditions.Value;

        public virtual async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

        public virtual async Task DisposeAsync()
        {
            if (_preconditions.IsValueCreated)
            {
                await foreach (var precondition in _preconditions.Value.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }

            await Task.CompletedTask;
        }
    }
}