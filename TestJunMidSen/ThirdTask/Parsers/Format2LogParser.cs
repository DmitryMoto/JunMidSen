using System.Globalization;
using System.Text.RegularExpressions;
using TestJunMidSen.ThirdTask.Interfaces;
using TestJunMidSen.ThirdTask.Models;
using TestJunMidSen.ThirdTask.Utils;

namespace TestJunMidSen.ThirdTask.Parsers
{
    public class Format2LogParser : ILogParser
    {
        private readonly Regex _regex = new(@"^(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}\.\d+)\|\s*(?<level>INFO|INFORMATION|WARN|WARNING|ERROR|DEBUG)\|\d+\|(?<method>[^|]+)\|\s*(?<message>.+)$");

        public bool TryParse(string line, out LogStruct? logStruct)
        {
            logStruct = null;
            var match = _regex.Match(line);
            if (!match.Success) return false;

            if (!DateTime.TryParseExact(match.Groups["date"].Value, "yyyy-MM-dd", null, DateTimeStyles.None, out var date))
                return false;

            logStruct = new LogStruct
            {
                Date = date.ToString("dd-MM-yyyy"),
                Time = match.Groups["time"].Value,
                LogLevel = LogLevelNormalizer.Normalize(match.Groups["level"].Value),
                CallerMethod = match.Groups["method"].Value.Trim(),
                Message = match.Groups["message"].Value
            };

            return true;
        }
    }
}
