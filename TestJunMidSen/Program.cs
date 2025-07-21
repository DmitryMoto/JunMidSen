// See https://aka.ms/new-console-template for more information
using TestJunMidSen.ThirdTask.Interfaces;
using TestJunMidSen.ThirdTask.Output;
using TestJunMidSen.ThirdTask.Parsers;
using TestJunMidSen.ThirdTask.Processing;
using TestJunMidSen.ThirdTask.Utils;

Console.WriteLine("Hello, World!");

Console.WriteLine("Введите имя лог-файла для стандартизации");
string? inputFileLog = Console.ReadLine();

if (string.IsNullOrWhiteSpace(inputFileLog))
{
    Console.WriteLine("Ошибка: имя файла не может быть пустым.");
    return;
}

if (!File.Exists(inputFileLog))
{
    Console.WriteLine($"Ошибка: файл '{inputFileLog}' не существует.");
    return;
}

var parsers = new List<ILogParser>
        {
            new Format1LogParser(),
            new Format2LogParser()
        };

var writer = new FileOutputWriter(GetPrefixedPath.Get(inputFileLog, "parsed-"), "problems.txt");
var processor = new LogProcessor(parsers, writer);

using (var reader = new StreamReader(inputFileLog))
{
    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        processor.ProcessLine(line);
    }
}

writer.Close();
Console.WriteLine("Обработка завершена.");