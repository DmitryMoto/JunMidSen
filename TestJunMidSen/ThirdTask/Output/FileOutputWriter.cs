using TestJunMidSen.ThirdTask.Interfaces;
using TestJunMidSen.ThirdTask.Models;

namespace TestJunMidSen.ThirdTask.Output
{
    public class FileOutputWriter : IOutputWriter
    {
        private readonly StreamWriter _validWriter;
        private readonly StreamWriter _invalidWriter;

        public FileOutputWriter(string validPath, string invalidPath)
        {
            _validWriter = new StreamWriter(validPath);
            _invalidWriter = new StreamWriter(invalidPath);
        }

        public void WriteValid(LogStruct logStruct)
        {
            var line = $"{logStruct.Date}\t{logStruct.Time}\t{logStruct.LogLevel}\t{logStruct.CallerMethod}\t{logStruct.Message}";
            _validWriter.WriteLine(line);
        }

        public void WriteInvalid(string line)
        {
            _invalidWriter.WriteLine(line);
        }

        public void Close()
        {
            _validWriter.Dispose();
            _invalidWriter.Dispose();
        }
    }
}
