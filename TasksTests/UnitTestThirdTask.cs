using FluentAssertions;
using System.Reflection.Metadata;
using TestJunMidSen.ThirdTask.Interfaces;
using TestJunMidSen.ThirdTask.Models;
using TestJunMidSen.ThirdTask.Parsers;
using TestJunMidSen.ThirdTask.Processing;

namespace TasksTests
{
    public class MemoryOutputWriter : IOutputWriter
    {
        private readonly List<string> _valid = [];
        private readonly List<string> _invalid = [];

        public void WriteValid(LogStruct logStruct)
        {
            var line = $"{logStruct.Date}\t{logStruct.Time}\t{logStruct.LogLevel}\t{logStruct.CallerMethod}\t{logStruct.Message}";
            _valid.Add(line);
        }

        public void WriteInvalid(string line)
        {
            _invalid.Add(line);
        }

        public void Close() { }

        public IReadOnlyList<string> ValidLines => _valid;
        public IReadOnlyList<string> InvalidLines => _invalid;
    }

    public class UnitTestThirdTask
    {
        [Fact]
        public void Process_MixedLogLines_CorrectlySeparatesValidAndInvalid()
        {
            // Arrange
            var lines = new[]
            {
            "10.03.2025 15:14:49.523 INFORMATION  Версия программы: '3.4.0.48729'",
            "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'",
            "",
            "НЕВАЛИДНАЯ СТРОКА БЕЗ ВСЕГО",
            "10.03.2025 BADTIME INFORMATION Сообщение без времени",
            "BADDATE 15:14:49.523 INFORMATION Сообщение без даты",
            "2025-03-10 15:14:51.5882| |11|| Пустой уровень логирования",
            "2025-03-10 15:14:51.5882| DEBUG|11|System.Diagnostics.Logger| Отладка начата"
        };

            var parsers = new List<ILogParser> { new Format1LogParser(), new Format2LogParser() };
            var writer = new MemoryOutputWriter();
            var processor = new LogProcessor(parsers, writer);

            // Act
            foreach (var line in lines)
            {
                processor.ProcessLine(line);
            }

            // Assert
            writer.ValidLines.Should().HaveCount(3);
            writer.InvalidLines.Should().HaveCount(5);

            writer.ValidLines.Count(l => l.Contains("INFO")).Should().Be(2);
            writer.ValidLines.Count(l => l.Contains("DEBUG")).Should().Be(1);
            writer.ValidLines.Should().Contain(l => l.Contains("MobileComputer") || l.Contains("DEFAULT"));
        }
    }
}
