namespace BuddyApiClient.IntegrationTest
{
    using System;
    using System.Threading.Tasks;

    public abstract class Precondition : IAsyncDisposable
    {
        private bool _disposed;

        public async ValueTask DisposeAsync()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask Dispose(bool disposing)
        {
            _disposed = true;

            await Task.CompletedTask;
        }

        protected void ThrowIfDisposed()
        {
            if (!_disposed)
            {
                return;
            }

            throw new ObjectDisposedException(GetType().Name);
        }
    }

    public abstract class Precondition<T> : Precondition
    {
        private readonly Func<Task<T>> _arrange;
        private T? _value;

        protected Precondition(Func<Task<T>> arrange)
        {
            _arrange = arrange;
        }

        protected bool HasBeenArranged { get; private set; }

        public async Task<T> Arrange()
        {
            if (HasBeenArranged && _value is not null)
            {
                return _value;
            }

            ThrowIfDisposed();

            _value = await _arrange();

            HasBeenArranged = true;

            return _value;
        }
    }
}