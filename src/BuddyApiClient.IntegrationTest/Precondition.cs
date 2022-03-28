namespace BuddyApiClient.IntegrationTest
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class Precondition
    {
        public abstract Task SetUp();

        public virtual async Task TearDown()
        {
            await Task.CompletedTask;
        }
    }

    internal abstract class Precondition<T> : Precondition where T : struct
    {
        private readonly Func<Task<T>> _setUp;
        private readonly SemaphoreSlim _setUpLock;
        private T? _value;

        protected Precondition(Func<Task<T>> setUp)
        {
            _setUp = setUp;
            _setUpLock = new SemaphoreSlim(1, 1);
        }

        protected bool IsSetUp => _value.HasValue;

        public override async Task<T> SetUp()
        {
            if (_value.HasValue)
            {
                return _value.Value;
            }

            await _setUpLock.WaitAsync();

            try
            {
                _value ??= await _setUp();
            }
            finally
            {
                _setUpLock.Release();
            }

            return _value.Value;
        }
    }
}