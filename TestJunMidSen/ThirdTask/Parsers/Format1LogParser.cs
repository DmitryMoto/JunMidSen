using System.Globalization;
using System.Text.RegularExpressions;
using TestJunMidSen.ThirdTask.Interfaces;
using TestJunMidSen.ThirdTask.Models;
using TestJunMidSen.ThirdTask.Utils;

namespace TestJunMidSen.ThirdTask.Parsers
{
    public class Format1LogParser : ILogParser
    {
        private readonly Regex _regex = new(@"^(?<date>\d{2}\.\d{2}\.\d{4}) (?<time>\d{2}:\d{2}:\d{2}\.\d{3})\s+(?<level>INFORMATION|WARNING|ERROR|DEBUG)\s+(?<message>.+)$");

        public bool TryParse(string line, out LogStruct? logStruct)
        {
            logStruct = null;
            var match = _regex.Match(line);
            if (!match.Success) return false;

            if (!DateTime.TryParseExact(match.Groups["date"].Value, "dd.MM.yyyy", null, DateTimeStyles.None, out var date))
                return false;

            logStruct = new LogStruct
            {
                Date = date.ToString("dd-MM-yyyy"),
                Time = match.Groups["time"].Value,
                LogLevel = LogLevelNormalizer.Normalize(match.Groups["level"].Value),
                Message = match.Groups["message"].Value
            };
            return true;
        }
    }

}
