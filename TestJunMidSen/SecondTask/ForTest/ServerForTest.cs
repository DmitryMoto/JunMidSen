namespace TestJunMidSen.SecondTask.ForTest
{
    public class ServerForTest
    {
        private int _count = 0;
        private readonly ILockWrapper _lock;

        public ServerForTest(ILockWrapper @lock)
        {
            _lock = @lock;
        }

        public bool GetCount(out int count, int timeoutMs = 300)
        {
            if (_lock.TryEnterReadLock(timeoutMs))
            {
                try
                {
                    count = _count;
                    return true;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
            count = default;
            return false;
        }

        public bool AddToCount(int count, int timeoutMs = 300)
        {
            if (_lock.TryEnterWriteLock(timeoutMs))
            {
                try
                {
                    checked
                    {
                        _count += count;
                    }
                    return true;
                }
                catch (OverflowException)
                {
                    return false;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
            return false;
        }

        public void ResetCount() => _count = 0;
    }
}
