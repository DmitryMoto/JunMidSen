namespace TestJunMidSen.SecondTask
{
    public static class Server
    {
        private static int _count = 0;
        private static readonly ReaderWriterLockSlim _lock = new();

        public static bool GetCount(out int count, int timeoutMs = 300)
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

        public static bool AddToCount(int count, int timeoutMs = 300)
        {
            if(_lock.TryEnterWriteLock(timeoutMs))
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
    }
}
