namespace TestJunMidSen.SecondTask.ForTest
{
    public class ReaderWriterLockWrapper : ILockWrapper
    {
        private readonly ReaderWriterLockSlim _lock = new();

        public bool TryEnterReadLock(int timeoutMs) => _lock.TryEnterReadLock(timeoutMs);
        public void ExitReadLock() => _lock.ExitReadLock();

        public bool TryEnterWriteLock(int timeoutMs) => _lock.TryEnterWriteLock(timeoutMs);
        public void ExitWriteLock() => _lock.ExitWriteLock();
    }
}
