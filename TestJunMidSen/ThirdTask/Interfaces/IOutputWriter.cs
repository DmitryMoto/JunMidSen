using TestJunMidSen.ThirdTask.Models;

namespace TestJunMidSen.ThirdTask.Interfaces
{
    public interface IOutputWriter
    {
        public void WriteValid(LogStruct logStruct);
        public void WriteInvalid(string line);
        public void Close();
    }
}
