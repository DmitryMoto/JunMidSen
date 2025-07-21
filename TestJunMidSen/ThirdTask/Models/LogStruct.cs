namespace TestJunMidSen.ThirdTask.Models
{
    public class LogStruct
    {
        //Дата
        public string Date { get; set; }
        //Время
        public string Time { get; set; }
        //Уровень логирования
        public string LogLevel { get; set; }
        //Вызвавший метод
        public string CallerMethod { get; set; } = "DEFAULT";
        //Сообщение
        public string Message { get; set; }
    }
}
