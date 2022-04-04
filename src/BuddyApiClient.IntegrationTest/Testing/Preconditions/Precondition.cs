namespace BuddyApiClient.IntegrationTest.Testing.Preconditions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class Precondition
    {
        public abstract Task SetUp();

        public abstract Task TearDown();
    }

    internal abstract class Precondition<T> : Precondition, IDisposable where T : struct
    {
        private readonly Func<Task<T>> _setUp;
        private readonly Func<Func<Task<T>>, Func<Task>> _tearDown;
        private readonly SemaphoreSlim _valueLock;
        private bool _disposed;
        private T? _value;

        protected Precondition(Func<Task<T>> setUp, Func<Func<Task<T>>, Func<Task>> tearDown)
        {
            _setUp = setUp;
            _tearDown = tearDown;
            _valueLock = new SemaphoreSlim(1, 1);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override async Task<T> SetUp()
        {
            if (_value.HasValue)
            {
                return _value.Value;
            }

            await _valueLock.WaitAsync();

            try
            {
                _value ??= await _setUp();
            }
            finally
            {
                _valueLock.Release();
            }

            return _value.Value;
        }

        public override async Task TearDown()
        {
            if (!_value.HasValue)
            {
                return;
            }

            await _valueLock.WaitAsync();

            try
            {
                if (_value.HasValue)
                {
                    await _tearDown(SetUp)();

                    _value = null;
                }
            }
            finally
            {
                _valueLock.Release();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _valueLock.Dispose();
            }

            _disposed = true;
        }
    }
}