namespace TestJunMidSen.SecondTask.ForTest
{
    public interface ILockWrapper
    {
        bool TryEnterReadLock(int timeoutMs);
        void ExitReadLock();

        bool TryEnterWriteLock(int timeoutMs);
        void ExitWriteLock();
    }
}
