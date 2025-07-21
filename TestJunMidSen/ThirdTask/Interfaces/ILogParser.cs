using TestJunMidSen.ThirdTask.Models;

namespace TestJunMidSen.ThirdTask.Interfaces
{
    public interface ILogParser
    {
        bool TryParse(string line, out LogStruct? logStruct);
    }
}
