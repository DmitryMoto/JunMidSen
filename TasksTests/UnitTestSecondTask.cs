using FluentAssertions;
using Moq;
using TestJunMidSen.SecondTask.ForTest;

namespace TasksTests
{
    public class UnitTestSecondTask
    {
        [Fact]
        public async Task MultipleReaders_CanReadSimultaneously_Async()
        {
            // Arrange
            var server = new ServerForTest(new ReaderWriterLockWrapper());
            server.ResetCount();

            var reader1Started = new TaskCompletionSource();
            var reader2Started = new TaskCompletionSource();
            var bothReadersCanProceed = new TaskCompletionSource();

            bool reader1Success = false;
            bool reader2Success = false;

            var reader1 = Task.Run(async () =>
            {
                reader1Started.SetResult();
                await bothReadersCanProceed.Task;
                reader1Success = server.GetCount(out _, timeoutMs: 500);
            });

            var reader2 = Task.Run(async () =>
            {
                reader2Started.SetResult();
                await bothReadersCanProceed.Task;
                reader2Success = server.GetCount(out _, timeoutMs: 500);
            });

            await Task.WhenAll(reader1Started.Task, reader2Started.Task);

            bothReadersCanProceed.SetResult();

            await Task.WhenAll(reader1, reader2);

            // Assert
            reader1Success.Should().BeTrue("первый читатель должен захватить read-lock");
            reader2Success.Should().BeTrue("второй читатель должен захватить read-lock одновременно с первым");
        }

        [Fact]
        public async Task Writer_Should_Be_Blocked_When_WriteLock_Is_Not_Available()
        {
            // Arrange
            var mockLock = new Mock<ILockWrapper>();

            var firstCall = true;
            mockLock.Setup(l => l.TryEnterWriteLock(It.IsAny<int>()))
                    .Returns(() =>
                    {
                        if (firstCall)
                        {
                            firstCall = false;
                            return true;
                        }
                        return false;
                    });

            var server = new ServerForTest(mockLock.Object);

            // Act
            var results = await Task.Run(() =>
            {
                var firstResult = server.AddToCount(1);
                var secondResult = server.AddToCount(1);
                return (firstResult, secondResult);
            });

            // Assert
            results.firstResult.Should().BeTrue("первая попытка захвата write-lock должна быть успешной");
            results.secondResult.Should().BeFalse("вторая попытка должна быть заблокирована");
            mockLock.Verify(l => l.TryEnterWriteLock(It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Reader_Should_Be_Blocked_When_WriteLock_Is_Held()
        {
            // Arrange
            var mockLock = new Mock<ILockWrapper>();

            mockLock.Setup(l => l.TryEnterWriteLock(It.IsAny<int>())).Returns(true);
            mockLock.Setup(l => l.TryEnterReadLock(It.IsAny<int>())).Returns(false);

            var server = new ServerForTest(mockLock.Object);

            // Act
            var results = await Task.Run(() =>
            {
                var writeSuccess = server.AddToCount(1);
                var readSuccess = server.GetCount(out _);
                return (writeSuccess, readSuccess);
            });

            // Assert
            results.writeSuccess.Should().BeTrue();
            results.readSuccess.Should().BeFalse("читатель не должен получить read-lock, пока write-lock удерживается");
        }
    }

}

