using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestJunMidSen.ThirdTask.Interfaces;

namespace TestJunMidSen.ThirdTask.Processing
{
    public class LogProcessor : ILogProcessor
    {
        private readonly List<ILogParser> _parsers;
        private readonly IOutputWriter _writer;

        public LogProcessor(IEnumerable<ILogParser> parsers, IOutputWriter writer)
        {
            _parsers = parsers.ToList();
            _writer = writer;
        }

        public void ProcessLine(string line)
        {
            foreach (var parser in _parsers)
            {
                if (parser.TryParse(line, out var logStruct))
                {
                    _writer.WriteValid(logStruct!);
                    return;
                }
            }

            _writer.WriteInvalid(line);
        }
    }
}
